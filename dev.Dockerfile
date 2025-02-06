FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm
# vscode is a built-in user in the image
# for other username do UNIX create user
COPY --chown=vscode:vscode ./ /workspaces/TableDriver
