// import axios from 'axios';
// import qs from 'qs';

// const API_CLIENT_ID = 'ro.client';
// const API_CLIENT_SECRET = 'secret';
// const API_BASE_URL = "http://localhost:5002";

// async getAccessToken(credentials) {
//     const axiosConfig = {
//         baseURL: API_BASE_URL,
//         timeout: 30000,
//         headers: {
//             'Content-Type': 'application/x-www-form-urlencoded'
//         }
//     };

//     const requestData = {
//         client_id: API_CLIENT_ID,
//         client_secret: API_CLIENT_SECRET,
//         grant_type: 'password',
//         username: credentials.username,
//         password: credentials.password,
//         scope: 'api1'
//     };

//     try {
//         const result = await axios.post('/connect/token', qs.stringify(requestData), axiosConfig);
//         return result.response;
//     } catch (err) {
//         return err;
//     }
// }

import UserModel from "@/common/UserModel";
import { deserialize } from "@/Helpers/JsonMapper";
import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import { authStore } from "@/stores/AuthStore";
import { serialize, DeserializeArray } from "@/helpers/JsonMapper";
import mgr from "@/Security/security";
import type { User } from "oidc-client";
import router from "@/router";

const API_CLIENT_ID = "RadarLiteClient";
const API_CLIENT_SECRET = "0/6t7wnncRj4pwHTXkh6tGF8vpIYsr2YQsMWIB4sTbY=";
const IDENTITY_URL = "https://localhost:7056/";
const LOGIN_API_URL = "https://localhost:5000";
const config: AxiosRequestConfig = {
  baseURL: IDENTITY_URL,
  timeout: 25000,
  headers: {
    //"Authorization": `Bearer ${elasticPrivateKey}`,
    "Content-Type": "application/x-www-form-urlencoded",
  },
};
const login_config: AxiosRequestConfig = {
  baseURL: LOGIN_API_URL,
  timeout: 25000,
  headers: {
    //"Authorization": `Bearer ${elasticPrivateKey}`,
    "X-CSRF": "1",
  },
};

export async function getAccessToken(user: UserModel) {
  const requestData = {
    client_id: API_CLIENT_ID,
    grant_type: "code",
    client_secret: API_CLIENT_SECRET,
    username: user.username,
    password: user.password,
    //scope: ["NWS.Wind", "NWS.Temperature"],
    //scope: "NWS.Wind",
  };
  try {
    const result = await axios.get<JsonMapper.IGenericObject>(
      "/bff/login?returnUrl=/home",
      config
    );
    console.log(result.response);
    return result.response;
  } catch (err) {
    return err;
  }
}
export async function authenticate(returnpath: string): Promise<boolean> {
  const user = await getUser();
  if (!!user) {
    return true;
  }
  return false;
}
export async function getUser() {
  try {
    let user = await mgr.getUser();
    return user;
  } catch (err) {
    console.log(err);
  }
}
export function signIn(returnPath: string) {
  returnPath ? mgr.signinRedirect({ state: returnPath }) : mgr.signinRedirect();
}

export async function Login(user: UserModel) {
  axios.defaults.timeout = 25000;
  const path = "https://localhost:44317/nws/healthy";
  //let response: AxiosResponse<JsonMapper.IGenericObject>;
}

export async function CreateNewUser(newUser: UserModel): Promise<boolean> {
  try {
    //set axios up, injectors?
    //Create the path, call the enpoint with Post
    //Server returns true if it was successful
    //else throw an error?
    return true;
  } catch (e) {
    return false;
  }
}
