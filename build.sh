set -eu pipefail
dotnet run --project build -- "$@"
