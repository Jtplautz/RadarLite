//https://medium.com/geekculture/how-to-strongly-type-try-catch-blocks-in-typescript-4681aff406b9
// Default ErrorResponse class.
export class ErrorResponse {
  constructor(public message: string, public status: number) {}
}

export class DbConcurrencyError implements ErrorResponse {
  constructor(public message: string, public status: number) {}
}

export class InternalServerError implements ErrorResponse {
  constructor(public message: string, public status: number) {}
}

export class UnauthorizedError implements ErrorResponse {
  constructor(public message: string, public status: number) {}
}
