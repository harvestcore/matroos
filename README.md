# Matroos

Create and deploy Discord bots easily.

|                        | Status                                                       |
| ---------------------- | ------------------------------------------------------------ |
| CI                     | [![Unit tests](https://github.com/harvestcore/matroos/actions/workflows/test-dotnet.yml/badge.svg)](https://github.com/harvestcore/matroos/actions/workflows/test-dotnet.yml) |
| Release                | [![Release](https://img.shields.io/github/v/release/harvestcore/matroos)](https://github.com/harvestcore/matroos/releases) |
| License                | [![License](https://img.shields.io/github/license/harvestcore/matroos)](https://www.gnu.org/licenses/gpl-3.0) |
| Documentation          | [![​ - ​Markdown](https://img.shields.io/badge/Markdown-2ea4ff?logo=markdown)](./doc/md/README.md) [![​ - ​Markdown](https://img.shields.io/badge/LaTeX-2ea4ff?logo=latex)](./doc/tex/README.md) [![PDF - Latest artifact](https://img.shields.io/badge/PDF_Latest_artifact-2ea4ff?logo=adobe-acrobat-reader)](https://github.com/harvestcore/matroos/actions/workflows/build-latex.yml) |
| PDF Build & Spellcheck | [![PDF build & spellcheck](https://github.com/harvestcore/matroos/actions/workflows/build-latex.yml/badge.svg)](https://github.com/harvestcore/matroos/actions/workflows/build-latex.yml) |
| API reference          | [![API](https://img.shields.io/badge/-API-informational)](./doc/md/api.md) |

## How to run the software

### Backend & workers

Using Docker:
```sh
docker-compose up --build
```

Using `dotnet`:
```sh
# Compile backend
cd backend
dotnet restore
dotnet build --no-restore -c Release
dotnet publish -c Release -o ./app --no-restore

# Compile worker
cd worker
dotnet restore
dotnet build --no-restore -c Release
dotnet publish -c Release -o ./app --no-restore

# Run backend
cd backend/app
dotnet Matroos.Backend.dll

# Run worker
cd worker/app
dotnet Matroos.Worker.dll
```

Using Visual Studio:
- Open the solution backend and worker solution (`.sln`) files.

### Frontend

Using Docker:
```sh
docker-compose up --build
```

Using `npm`:
```sh
cd matroos/frontend

# Install dependencies.
npm install

# Run a development environment.
npm run start

# Build a production app.
npm run build
```
