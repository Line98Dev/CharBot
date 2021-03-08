echo Pulling any changes
git pull
echo Stopping CharBot service
sudo systemctl --user stop charbot
echo Building CharBot
dotnet publish -c release -r ubuntu.20.10-arm64 --self-contained
echo CharBot built
sudo systemctl --user start charbot
echo service started