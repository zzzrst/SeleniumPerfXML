curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/Chromium.zip > chromium.zip
unzip -u Chromium.zip
DEL Chromium.zip
curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/Release.zip > Release.zip
unzip -u Release.zip
DEL Release.zip
MOVE Chromium Release/netcoreapp3.1
curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/GetLatestRelease.bat > GetLatestRelease.bat