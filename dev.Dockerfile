FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

COPY --chown=develop:develop ./ /workspaces/TableDriver
