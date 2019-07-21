#!/bin/bash
chmod -R a+x build
cd ./build
rm -rf .fake packages paket-files/paket.restore.cached paket.lock build.fsx.lock
./fake.sh run build.fsx --target $1