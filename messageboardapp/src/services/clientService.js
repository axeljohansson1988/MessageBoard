import { makeGetRequest, makePostequest } from "./apiService";

export const getClients = () => {
    var relativeUrl = `Client`;
    return makeGetRequest(relativeUrl);
};

export const addClient = (client) => {
    var relativeUrl = `Client`;
    return makePostequest(relativeUrl, client);
};