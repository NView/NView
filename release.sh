
#
# This script will build a release version of the library,
# package it,
# then push it to nuget.
# You should then commit and tag any changes.
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


echo Please commit any changes then tag this release with \"git tag -a v$VER\"
