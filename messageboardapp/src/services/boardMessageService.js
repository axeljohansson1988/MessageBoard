import { makeDeleteequest, makeGetRequest, makePostequest, makePutRequest } from "./apiService";

export const getBoardMessages = (clientId, latestFirst) => {
    var relativeUrl = `BoardMessage/${clientId}/${latestFirst}`;
    return makeGetRequest(relativeUrl);
};

export const addBoardMessage = (boardMessage) => {
    var relativeUrl = `BoardMessage`;
    return makePostequest(relativeUrl, boardMessage);
};

export const updateBoardMessage = (boardMessage) => {
    var relativeUrl = `BoardMessage`;
    return makePutRequest(relativeUrl, boardMessage);
};

export const deleteBoardMessage = (boardMessageId, clientId) => {
    var relativeUrl = `BoardMessage/${boardMessageId}/${clientId}`;
    return makeDeleteequest(relativeUrl);
};