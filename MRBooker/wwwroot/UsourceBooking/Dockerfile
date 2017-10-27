FROM node

WORKDIR /app

# install gulp, slush, and custom generators
RUN npm install --global \
      gulp-cli \
      slush \
      slush-atom \
      slush-molecule

# install node modules
COPY package.json /app/package.json
COPY npm-shrinkwrap.json /app/npm-shrinkwrap.json
RUN npm install

# add project files to image
COPY . /app

# temporary fix for deprecated slush generators
RUN echo "[user]\nname=\nemail=" >> /root/.gitconfig

# build project
RUN gulp patternlab:build

# set default command to run server
CMD gulp patternlab:serve
