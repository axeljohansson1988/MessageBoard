import React from "react";
import { Button, Col, Container, Row } from "react-bootstrap";
import { IClient } from "../models/apiModels";
import { IBoardMessage } from "../models/apiModels";
import { deleteBoardMessage, getBoardMessages } from "../services/boardMessageService";
import { getClients } from "../services/clientService";


interface IBoardProps {

}
interface IBoardState {
    currentClient?: IClient;
    allClients: IClient[];
    boardMessages: IBoardMessage[];
    latestMessageOnTop: boolean;
    showNewClientInput: boolean;
}

export default class Board extends React.Component<IBoardProps, IBoardState> {
    constructor(props: IBoardProps) {
        super(props);
        this.state = {
            currentClient: undefined,
            allClients: [],
            boardMessages: [],
            latestMessageOnTop: true,
            showNewClientInput: false
        };
    };

    componentDidMount() {
        const { currentClient } = this.state;
        if (!currentClient) {
            this.setState({ showNewClientInput: true });
        } else {
            this.getClients();
            this.getBoardMessages();
        }
    }

    getBoardMessages = () => {
        const { currentClient, latestMessageOnTop } = this.state;
        if (!currentClient) {
            return;
        }
        getBoardMessages(currentClient.id, latestMessageOnTop).then((response) => {
            this.setState({ boardMessages: response.data });
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

    // getNewClientInput = (): JSX.Element => {
    //     return <React.Fragment>
    //         <input type="text" onChange={this.handleInputChange} />
    //     </React.Fragment>
    // }

    getBoard = (): JSX.Element => {
        const { boardMessages } = this.state;
        let board: JSX.Element = (
            <Container>
                {boardMessages.map((boardMessage: IBoardMessage, index: number) => {
                    return (<Row key={index}>
                        <Col xs={6}>
                            {boardMessage.message}
                        </Col>
                        <Col xs={2}>
                            {boardMessage.clientId}
                        </Col>
                        <Col xs={2}>
                            {boardMessage.isMine &&
                                <Button variant="primary">
                                    Edit
                            </Button>
                            }
                        </Col>
                        <Col xs={2}>
                            {boardMessage.isMine &&
                                <Button variant="primary" onClick={() => this.deleteBoardMessage(boardMessage.id)}>
                                    Delete
                            </Button>
                            }
                        </Col>
                    </Row>)
                })}
            </Container>
        );
        return board;
    }

    render = (): JSX.Element => {
        return (
            <div>
                hej board
                {this.getBoard()}
            </div>
        );
    }
}