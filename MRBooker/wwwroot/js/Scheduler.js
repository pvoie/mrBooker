﻿$(document).ready(function () {

    $("#RoomId").change(function () {
        var roomid = $('#RoomId option:selected').val();
        setRoom(roomid);
    });

    function setRoom(roomid) {
        $('#roomId').val(roomid)
            .trigger('change');
    }

    var selectedRooms = [];
    $.getJSON("api/reservationApi/GetAllRooms",
        function (data) {
            $.each(data,
                function (i, item) {
                    selectedRooms.push(item);
                });
        });

    scheduler.config.xml_date = "%Y/%m/%d %h:%i:%s";
    scheduler.config.limit_time_select = true;
    scheduler.config.first_hour = 8;
    scheduler.config.last_hour = 22;
    scheduler.config.server_utc = false;
    scheduler.config.readonly = false;
    scheduler.locale.labels.map_tab = "Map";
    scheduler.locale.labels.section_location = "Location";

    var selectedRoom = [
        { key: $('#RoomId option:selected').val(), label: $('#RoomId option:selected').text() }
    ];
    var lbSections = [
        { name: "Title", height: 30, map_to: "title", type: "textarea", focus: true },
        { name: "Description", height: 30, map_to: "description", type: "textarea", focus: true },
        { name: "Location", height: 43, map_to: "event_location", type: "textarea" },
        { name: "Status", height: 30, map_to: "status", type: "textarea", focus: true },
        { name: "room", height: 58, options: selectedRoom, map_to: "room", type: "radio", vertical: true },
        { name: "time", height: 72, type: "time", map_to: "auto", time_format: ["%H:%i", "%d", "%m", "%Y"] }
    ];

    scheduler.templates.event_bar_text = function (start, end, ev) {
        return ev.title;
    };
    scheduler.templates.tooltip_text = function (start, end, ev) {
        return "<b>Event:</b> " + ev.title + "<br/><b>Start date:</b> " +
            scheduler.templates.tooltip_date_format(start) +
            "<br/><b>End date:</b> " + scheduler.templates.tooltip_date_format(end);
    };
    scheduler.locale.labels.section_room = 'Room';
    scheduler.config.lightbox.sections = lbSections;
    scheduler.load("api/reservationApi/GetAll", "json");
    scheduler.init('main_scheduler', new Date(), "month");

    

    $('#roomId').change(function () {
        scheduler.load("api/reservationApi/GetReservationByRoom/?roomId=" + $('#roomId').val(), "json");
        scheduler.clearAll();
        scheduler.setCurrentView();
    });
    
    scheduler.attachEvent("onBeforeDrag", function () {
        if ($('#RoomId option:selected').text() === "All Rooms") {
            dhtmlx.message({
                title: "Room",
                type: "alert-warning",
                text: "Please select a room before reservation"
            });
            return false;
        }
        return true;
    });
 
    scheduler.attachEvent("onBeforeLightbox", function () {
        scheduler.resetLightbox();
            lbSections = [
                { name: "Title", height: 30, map_to: "title", type: "textarea", focus: true },
                { name: "Description", height: 30, map_to: "description", type: "textarea", focus: true },
                { name: "Status", height: 30, map_to: "status", type: "textarea", focus: true },
                { name: "Location", height: 43, map_to: "event_location", type: "textarea" },
                {
                    name: "room",
                    height: 58,
                    options: selectedRooms,
                    map_to: "roomId",
                    type: "select"
                },
                { name: "time", height: 72, type: "time", map_to: "auto", time_format: ["%H:%i", "%d", "%m", "%Y"] }
            ];
        scheduler.config.lightbox.sections = lbSections;
        return true;
    });

    scheduler.attachEvent("onEventSave",
        function (id, ev, is_new) {
            if (is_new) {
                $.ajax({
                    url: "api/reservationApi/insert",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(ev),
                    success: function () {
                        dhtmlx.message({
                            title: "Reservation",
                            type: "alert-warning",
                            text: "Reservation has been saved.",
                            callback: function () {
                                scheduler.clearAll();
                                scheduler.load("api/reservationApi/GetReservationByRoom/?roomId=" + $('#roomId').val(), "json");
                                scheduler.endLightbox(false, $('.dhx_cal_light').get(0));
                            }
                        });
                    },
                    error: function() {
                        dhtmlx.message({
                            title: "Reservation",
                            type: "alert-warning",
                            text: "The reservation has overlapped dates for this room."
                        });
                        return false;
                    }
                });
            } else {
                scheduler.updateEvent(id);
                scheduler.callEvent("onEventChanged", [id, ev]);
            }
        });

    scheduler.attachEvent("onEventChanged",
        function (id, ev) {
            var myarr = [
                {
                    id: id,
                    start_date: ev.start_date,
                    end_date: ev.end_date,
                    description: ev.description,
                    status: ev.status,
                    title: ev.title,
                    roomId: ev.roomId
                }
            ];
            $.ajax({
                url: "api/reservationApi/update",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(myarr[0]),
                success: function () {
                    dhtmlx.message({
                        title: "Reservation",
                        type: "alert-warning",
                        text: "Reservation has been updated.",
                        callback: function () {
                            scheduler.endLightbox(false, $('.dhx_cal_light').get(0));
                            scheduler.load("api/reservationApi/GetReservationByRoom/?roomId=" + $('#roomId').val(), "json");
                            scheduler.clearAll();
                            scheduler.setCurrentView();
                        }
                    });
                    return true;
                },
                error: function () {
                  
                    return false;
                }
            });
        });

    scheduler.attachEvent("onEventDeleted",
        function (id) {
            if (!scheduler.getState().new_event) {
                $.ajax({
                    url: "api/reservationApi/delete/?=" + id,
                    type: "DELETE",
                    contentType: 'application/json; charset=utf-8',
                    success: function(response) {
                        console.log('success !' + response);
                        return true;
                    },
                    error: function() {
                        console.log('error !' + response);
                        return false;
                    }
                });
            }
        });
    
});