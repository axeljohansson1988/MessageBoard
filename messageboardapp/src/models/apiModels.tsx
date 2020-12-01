interface IBaseModel {
    id?: number;
    created?: Date;
    modified?: Date;
};

interface IBaseResponse {
    status: string;
    operationSuccess: boolean;
}

export interface IBoardMessage extends IBaseModel {
    message: string;
    clientId?: number;
    isMine?: boolean;
};

export interface IClient extends IBaseModel {
    name: string;
};

export interface IBoardMessageReponse extends IBaseResponse {
    boardMessage: IBoardMessage;
};

export interface IClientReponse extends IBaseResponse {
    client: IClient;
};





