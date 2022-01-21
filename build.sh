set -o pipefail
dotnet run --project build -- "$@"
