import { React, useState } from "react";

import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    Button,
    Flex,
    useDisclosure,
    Grid,
    Box,
    Image
} from '@chakra-ui/react'

import { ClickableImageComponent } from "./ClickableImage"

import * as user1 from "../../pages/profile/img/user1.png"
import * as user2 from "../../pages/profile/img/user2.png"
import * as user3 from "../../pages/profile/img/user3.png"
import * as user4 from "../../pages/profile/img/user4.png"
import * as user5 from "../../pages/profile/img/user5.png"
import * as user6 from "../../pages/profile/img/user6.png"


function ProfileModalComponent() {
    const { isOpen, onOpen, onClose } = useDisclosure()
    console.log(user1)

    return (
        <>

            <Button bg={'blue.400'} onClick={onOpen}>Change Picture</Button>

            <Modal onClose={onClose} size={'lg'} isOpen={isOpen} isCentered>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Choose a Profile</ModalHeader>
                    <ModalCloseButton/>
                    <ModalBody>
                        <Grid templateColumns='repeat(3, 1fr)' gap={3} paddingTop="3em" paddingRight="3em" paddingBottom="5em">
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user1.default}
                                />
                            </Box>
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user2.default}
                                />
                            </Box>
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user3.default}
                                />
                            </Box>
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user4.default}
                                />
                            </Box>
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user5.default}
                                />
                            </Box>
                            <Box>
                                <ClickableImageComponent
                                    imgSrc={user6.default}
                                />
                            </Box>
                        </Grid>
                    </ModalBody>
                    <ModalFooter>
                        <Button bg={'blue.400'} onClick={onClose}>Select
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}

export function ModalComponent(profileName, description) {
    return <ProfileModalComponent/>
}