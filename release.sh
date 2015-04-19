#!/bin/bash
set -e

#
# This script will build a release binary,
# package it,
# push it to nuget,
# then tags the release in git and pushes it to origin.
#

APP=NView
VER=$1

if test -z "$1"; then
	echo "You need to specifiy the version number of $APP."
	exit 1
fi

echo Updating $APP files to 

sed -e "/<version>.*<\/version>/s//<version>$VER<\/version>/" -i ".previous" $APP.nuspec


echo Building $APP version $VER

xbuild /t:Clean /property:Configuration=Release $APP.sln
xbuild /t:Build /property:Configuration=Release $APP.sln


echo Publishing $APP version $VER

nuget pack $APP.nuspec
nuget push $APP.$VER.nupkg


echo Committing changes and tagging this release

git commit -am "Release $VER"
git tag -a v$VER -m "Release $VER"
git push --follow-tags



