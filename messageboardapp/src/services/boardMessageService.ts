import { IBoardMessage } from "../models/apiModels";
import { makeDeleteequest, makeGetRequest, makePostequest, makePutRequest } from "./apiService";

export const getBoardMessages = (latestFirst: boolean, clientId?: number) => {
    var relativeUrl = `BoardMessage/${clientId}/${latestFirst}`;
    return makeGetRequest(relativeUrl);
};

export const addBoardMessage = (boardMessage: IBoardMessage) => {
    var relativeUrl = `BoardMessage`;
    return makePostequest(relativeUrl, boardMessage);
};

export const updateBoardMessage = (boardMessage: IBoardMessage) => {
    var relativeUrl = `BoardMessage`;
    return makePutRequest(relativeUrl, boardMessage);
};

export const deleteBoardMessage = (boardMessageId?: number, clientId?: number) => {
    var relativeUrl = `BoardMessage/${boardMessageId}/${clientId}`;
    return makeDeleteequest(relativeUrl);
};