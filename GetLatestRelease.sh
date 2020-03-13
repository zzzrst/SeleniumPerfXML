curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/Chromium.zip > chromium.zip
unzip -u Chromium.zip
rm Chromium.zip
curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/Release.zip > Release.zip
unzip -u Release.zip
rm Release.zip
mv -u Chromium Release/netcoreapp3.1
curl -L https://github.com/zzzrst/SeleniumPerfXML/releases/latest/download/GetLatestRelease.sh > GetLatestRelease.sh