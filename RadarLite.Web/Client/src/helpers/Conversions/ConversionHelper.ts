//https://stackoverflow.com/a/44029696
export function convertToBoolean(input: string | boolean | number): boolean {
  try {
    return [
      true,
      "true",
      "True",
      "TRUE",
      "1",
      1,
      "on",
      "On",
      "ON",
      "ok",
      "Ok",
      "OK",
    ].includes(input);
  } catch (e) {
    //just return false and don't throw anything log that its bad respoonse
    console.log("Whoops! Looks like something is down unexpectedly.");
    return false;
    //log(e) to seq or to backend..?
  }
}
