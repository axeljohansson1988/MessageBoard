import moment from "moment";
import React from "react";
import { Button, Col, Container, Row, Table } from "react-bootstrap";
import { IBoardMessage } from "../models/apiModels";
import { IClient } from "../models/apiModels";
import { addBoardMessage, deleteBoardMessage, getBoardMessages, updateBoardMessage } from "../services/boardMessageService";
import { addClient, getClients } from "../services/clientService";


interface IBoardProps {

}
interface IBoardState {
    newClientName?: string;
    currentClient?: IClient;
    currentBoardMessage?: IBoardMessage;
    allClients: IClient[];
    boardMessages: IBoardMessage[];
    latestMessageOnTop: boolean;
    displaysNewClientInput: boolean;
    showMessageInput: boolean;
}
export default class Board extends React.Component<IBoardProps, IBoardState> {
    constructor(props: IBoardProps) {
        super(props);
        this.state = {
            newClientName: undefined,
            currentClient: undefined,
            currentBoardMessage: undefined,
            allClients: [],
            boardMessages: [],
            latestMessageOnTop: true,
            displaysNewClientInput: true,
            showMessageInput: false
        };
    };

    componentDidMount() {
        const { currentClient } = this.state;
        this.getClients();
        if (currentClient) {
            this.getBoardMessages();
        }
    }

    getBoardMessages = () => {
        const { currentClient, latestMessageOnTop } = this.state;
        if (!currentClient) {
            return;
        }
        getBoardMessages(currentClient.id, latestMessageOnTop).then((response) => {
            this.setState({ boardMessages: response.data, showMessageInput: true });
        }).catch((error) => {
            console.error(error);
        });
    }

    getClients = () => {
        getClients().then((response) => {
            this.setState({ allClients: response.data });
        }).catch((error) => {
            console.error(error);
        });
    }

    addNewClient = () => {
        const { newClientName } = this.state;
        if (!newClientName) {
            return;
        }
        let newClient: IClient = { name: newClientName };
        addClient(newClient).then((response) => {
            this.setState({ currentClient: response.data.client, boardMessages: [], displaysNewClientInput: false, newClientName: undefined }, () => this.getBoardMessages());
            this.getClients();
        }).catch((error) => {
            console.error(error);
        });
    }

    deleteBoardMessage = (boardMessageId?: number) => {
        const { currentClient } = this.state;
        if (!currentClient || !boardMessageId) {
            return;
        }
        deleteBoardMessage(boardMessageId, currentClient.id).then((response) => {
            this.getBoardMessages();
        }).catch((error) => {
            console.error(error);
        });
    }

    handleInputChange = (event: any) => {
        const { currentBoardMessage, currentClient } = this.state;
        let value: string = event.target.value;
        let inputName: string = event.target.name;
        switch (inputName) {
            case "clientName":
                this.setState({ newClientName: value });
                break;
            case "boardMessage":
                if (currentBoardMessage) {
                    this.setState({ currentBoardMessage: { ...currentBoardMessage, message: value } });
                } else if (currentClient) {
                    let boardMessage: IBoardMessage = { message: value, clientId: currentClient.id };
                    this.setState({ currentBoardMessage: boardMessage });
                }
                break;
            default:
                console.error("no input name found.");
                break;

        }
    }

    getNewClientContainer = (): JSX.Element => {
        const { newClientName } = this.state;
        return (<Container className={"py-3"}>
            <Row>
                <Col xs={8}>
                    <input type={"text"} name={"clientName"} className={"form-control"} placeholder={"Your name"} onChange={this.handleInputChange} />
                </Col>
                <Col xs={4}>
                    <Button variant={"primary"} type={"button"} disabled={!newClientName} onClick={this.addNewClient}>
                        Join the Board!
                    </Button>
                </Col>
            </Row>
        </Container>);
    }

    addOrUpdateBoardMessage = () => {
        const { currentBoardMessage } = this.state;
        if (!currentBoardMessage?.message) {
            return;
        }
        if (currentBoardMessage?.id) {
            updateBoardMessage(currentBoardMessage).then((response) => {
                this.setState({ currentBoardMessage: undefined, showMessageInput: false });
                this.getBoardMessages();
            }).catch((error) => {
                console.error(error);
            });
        } else {
            addBoardMessage(currentBoardMessage).then((response) => {
                this.setState({ currentBoardMessage: undefined, showMessageInput: false });
                this.getBoardMessages();
            }).catch((error) => {
                console.error(error);
            });
        }
    }

    getBoardMessageContainer = (): JSX.Element => {
        const { currentBoardMessage, currentClient, showMessageInput } = this.state;
        let buttonText: string = currentBoardMessage?.id ? "Update message" : "Add message"
        let messageContainer: JSX.Element = (
            <Container className={"py-3"}>
                <Row>
                    <Col xs={10}>
                        {showMessageInput &&
                            <input
                                type={"text"}
                                name={"boardMessage"}
                                className={"form-control"}
                                placeholder={`${currentClient?.name} says...`}
                                onChange={this.handleInputChange}
                                value={currentBoardMessage?.message}
                            />
                        }
                    </Col>
                    <Col xs={2}>
                        <Button
                            variant={"primary"}
                            type={"button"}
                            disabled={!currentBoardMessage?.message}
                            onClick={this.addOrUpdateBoardMessage}
                        >
                            {buttonText}
                        </Button>
                    </Col>
                </Row>
            </Container>);
        return messageContainer;
    }

    setCurrentBoardMessage = (boardMessage?: IBoardMessage) => {
        this.setState({ currentBoardMessage: boardMessage });
    }

    getClientNameById = (clientId?: number): string => {
        const { allClients } = this.state;
        let clientName: string = ""
        allClients.forEach((client: IClient) => {
            if (client.id === clientId) {
                clientName = client.name;
            }
        });

        return clientName;
    }

    setCurrentClient = (client?: IClient) => {
        if (client) {
            this.setState({ currentClient: client, displaysNewClientInput: false }, () => this.getBoardMessages());
        } else {
            this.setState({ currentClient: client, displaysNewClientInput: true });
        }
    }

    getFormattedDate = (date?: Date) => {
        if (!date) {
            return "";
        }
        return moment(date).format("HH:mm:ss");
    }

    getBoard = (): JSX.Element => {
        const { boardMessages } = this.state;
        let heading: JSX.Element = (
            <Row>
                <Col>
                    <h3>Message Board</h3>
                </Col>
            </Row>
        );
        let board: JSX.Element = (

            <Table responsive striped>
                <thead>
                    <tr>
                        <th>Message</th>
                        <th>Author</th>
                        <th>Created</th>
                        <th>Modified</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    <React.Fragment>
                        {boardMessages.map((boardMessage: IBoardMessage, index: number) => {
                            return (
                                <tr key={index}>
                                    <td>{boardMessage.message}</td>
                                    <td>{this.getClientNameById(boardMessage.clientId)}</td>
                                    <td>{this.getFormattedDate(boardMessage.created)}</td>
                                    <td>{this.getFormattedDate(boardMessage.modified)}</td>
                                    <td>{boardMessage.isMine &&
                                        <Button variant={"primary"} type={"button"} onClick={() => this.setCurrentBoardMessage(boardMessage)}>
                                            Edit
                                        </Button>
                                    }
                                    </td>
                                    <td>{boardMessage.isMine &&
                                        <Button variant={"primary"} type={"button"} onClick={() => this.deleteBoardMessage(boardMessage.id)}>
                                            Delete
                            </Button>
                                    }
                                    </td>

                                </tr>)
                        })}
                    </React.Fragment>
                </tbody>
            </Table>
        );

        return (
            <Container className={"py-3"}>
                {heading}
                {boardMessages.length === 0 ?
                    <Row>
                        <Col>
                            No messages in the board yet...
                    </Col>
                    </Row>
                    :
                    board
                }
            </Container>
        );
    }

    getClientList = () => {
        const { allClients, currentClient, displaysNewClientInput } = this.state;
        if (allClients.length === 0) {
            return <React.Fragment />;
        }
        let clientList: JSX.Element = (
            <Container className={"py-3"}>
                <Row>
                    <Col>
                        <h3>Select Client</h3>
                    </Col>
                </Row>
                {allClients.map((client: IClient, index: number) => {
                    return (<Row key={index}>
                        <Col xs={12}>
                            <span
                                className={`client-name ${currentClient?.id === client.id ? "font-weight-bold" : ""}`}
                                onClick={() => this.setCurrentClient(client)}
                            >
                                {client.name}
                            </span>
                        </Col>
                    </Row>)
                })
                }
                {!displaysNewClientInput &&
                    <Row>
                        <Col>
                            <Button type={"button"} variant={"primary"} onClick={() => this.setCurrentClient(undefined)}>
                                Add new Client
                            </Button>
                        </Col>
                    </Row>
                }
            </Container >
        );

        return clientList;
    }

    render = (): JSX.Element => {
        const { currentClient } = this.state;
        return (
            <div>
                {this.getClientList()}

                {!currentClient ?
                    this.getNewClientContainer()
                    :
                    <React.Fragment>
                        {this.getBoardMessageContainer()}
                        {this.getBoard()}
                    </React.Fragment>
                }
            </div>
        );
    }
}