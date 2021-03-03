echo Building CharBot
sudo systemctl stop CharBot
cd /CharBot/CharBot
dotnet publish -c release -r ubuntu.20.10-arm64 --self-contained
sudo systemctl start CharBot