import Axios from "axios"

const config = {
    headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
    }
};

const apiBaseUrl = "http://localhost:5000/api";
const getFullUrl = (relativeUrl) => {
    return `${apiBaseUrl}/${relativeUrl}`;
};

export const makeGetRequest = (relativeUrl) => {
    return Axios.get(getFullUrl(relativeUrl), config);
}

export const makePostequest = (relativeUrl, data) => {
    return Axios.post(getFullUrl(relativeUrl), data, config);
}

export const makePutRequest = (relativeUrl, data) => {
    return Axios.put(getFullUrl(relativeUrl), data, config);
}

export const makeDeleteequest = (relativeUrl) => {
    return Axios.delete(getFullUrl(relativeUrl), config);
}