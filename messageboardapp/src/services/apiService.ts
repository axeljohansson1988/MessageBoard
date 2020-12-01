import Axios from "axios"

const config = {
    headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
    }
};

const apiBaseUrl = "http://localhost:5000/api";
const getFullUrl = (relativeUrl: string) => {
    return `${apiBaseUrl}/${relativeUrl}`;
};

export const makeGetRequest = (relativeUrl: string) => {
    return Axios.get(getFullUrl(relativeUrl), config);
}

export const makePostequest = (relativeUrl: string, data: any) => {
    return Axios.post(getFullUrl(relativeUrl), data, config);
}

export const makePutRequest = (relativeUrl: string, data: any) => {
    return Axios.put(getFullUrl(relativeUrl), data, config);
}

export const makeDeleteequest = (relativeUrl: string) => {
    return Axios.delete(getFullUrl(relativeUrl), config);
}