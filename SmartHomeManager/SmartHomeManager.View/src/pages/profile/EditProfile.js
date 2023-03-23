import { React, useState, } from 'react';
import { useNavigate, useLocation } from "react-router-dom"
import {
    Button,
    Flex,
    FormControl,
    FormLabel,
    Heading,
    Input,
    Stack,
    useColorModeValue,
    HStack,
    Avatar,
    AvatarBadge,
    IconButton,
    Center,
    CloseButton,
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    useDisclosure,
} from '@chakra-ui/react';
import { ModalComponent } from "components/Profile/Modal";
import { SmallCloseIcon } from '@chakra-ui/icons';
import user1 from "./img/user1.png"

function EditProfile() {
    const [inputUserName, setInputUserName] = useState('');
    const [inputDescription, setInputDescription] = useState('');
    const [inputPin, setInputPin] = useState('');

    const { isOpen, onOpen, onClose } = useDisclosure()

    const [profileDetails, updateProfileDetails] = useState([])
    const location = useLocation();
    const profileId = location.state?.profileId;
    const getProfileDetails = async () => {
        await fetch(`https://localhost:7140/api/Profiles/${profileId}`, {
            method: 'GET',
            headers: {
                accept: 'text/plain'
            },
        })
            .then(async response => {
                const data = await response.json()

                if (response.ok) {
                    updateProfileDetails(data)
                }
            })
    }
    getProfileDetails()

    const handleUserNameChange = (event) => {
        setInputUserName(event.target.value)
    }

    const handleDescriptionChange = (event) => {
        setInputDescription(event.target.value)
    }

    const handlePinChange = (event) => {
        const pin = event.target.value.trim().slice(0, 4); // Trims whitespace and limits input to 4 characters
        setInputPin(pin || null);
    }

    const navigate = useNavigate();

    //JSO stringify to send to api controller
    if (inputPin == "") {
        setInputPin(null)
    }
    const editProfileObj = {
        "Name": inputUserName, "Description": inputDescription, "Pin": inputPin
    }

    //Use API to save into DB
    const handleSaveClick = async () => {
        const response = await fetch(`https://localhost:7140/api/Profiles/${profileId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(editProfileObj)
        })
        if (!response.ok) {
            throw new Error(`Failed to update profile: ${response.status}`);
        }
        else {
            //Navigate back to profile
            navigate(`/profiles`)
        }
    }

    //Use API to delete from DB
    //to replace with new id created
    const handleDeleteClick = async () => {
        const response = await fetch(`https://localhost:7140/api/Profiles/${profileId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        if (!response.ok) {
            throw new Error(`Failed to delete profile: ${response.status}`);
        }
        else {
            //Navigate back to profile
            navigate(`/profiles`)
        }
    }

    //Navigate to profiles page when closing the card
    const handleCloseClick = () => {
        //Navigate back to profile
        navigate(`/profiles`);
    }
    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack
                spacing={4}
                w={'full'}
                maxW={'md'}
                bg={useColorModeValue('white', 'gray.700')}
                rounded={'xl'}
                boxShadow={'lg'}
                p={6}
                my={12}>
                <Stack direction='row'>
                    <Flex justify="space-between" align="center" w="100%">
                        <Heading lineHeight={1.1} fontSize={{ base: '2xl', sm: '3xl' }} >
                            Edit {profileDetails.name}
                        </Heading>
                        <CloseButton onClick={handleCloseClick } />
                    </Flex>
                </Stack>
                
                <FormControl id="userName">
                    <FormLabel >Profile Picture</FormLabel>
                    <Stack direction={['column', 'row']} spacing={2}>
                        <Center>
                            <Avatar size="xl" src={user1} marginRight="15px" />
                        </Center>
                        <Center>
                            <ModalComponent />
                        </Center>
                    </Stack>
                </FormControl>
                <FormControl id="userName" isRequired>
                    <FormLabel>Name</FormLabel>
                    <Input
                        placeholder={profileDetails.name || "UserName" }
                        _placeholder={{ color: 'gray.500' }}
                        type="text"
                        value={inputUserName}
                        onChange={handleUserNameChange}
                    />
                </FormControl>
                <FormControl id="description" isRequired>
                    <FormLabel>Description</FormLabel>
                    <Input
                        placeholder={ profileDetails.description || "Enter the description here"}
                        _placeholder={{ color: 'gray.500' }}
                        type="text"
                        style={{ height: "150px", whiteSpace: "pre-wrap" }}
                        value={inputDescription}
                        onChange={handleDescriptionChange}
                    />
                </FormControl>
                <FormControl id="userPin">
                    <FormLabel>Set Pin</FormLabel>
                    <Input
                        placeholder={profileDetails.pin || "Set Parental Controls Here"}
                        _placeholder={{ color: 'gray.500' }}
                        type="number"
                        maxLength={4}
                        value={inputPin}
                        onChange={handlePinChange}
                    />
                </FormControl>


                <Stack spacing={6} direction={['column', 'row']}>
                    <Button
                        bg={'red.400'}
                        color={'white'}
                        w="full"
                        _hover={{
                            bg: 'red.500',
                        }}
                        onClick={onOpen}>
                        Delete
                    </Button>
                    <Modal isOpen={isOpen} onClose={onClose} isCentered>
                        <ModalOverlay />
                        <ModalContent>
                            <ModalHeader>Delete Profile</ModalHeader>
                            <ModalCloseButton />
                            <ModalBody>
                                Confirm that you want to delete the profile?
                            </ModalBody>

                            <ModalFooter>
                                <Button colorScheme='red' mr={3} onClick={handleDeleteClick}>
                                    Confirm
                                </Button>
                                <Button colorScheme='green' onClick={onClose}> Cancel</Button>
                            </ModalFooter>
                        </ModalContent>
                    </Modal>
                    <Button
                        bg={'blue.400'}
                        color={'white'}
                        w="full"
                        _hover={{
                            bg: 'blue.500',
                        }}
                        onClick={handleSaveClick}>
                        Save
                    </Button>
                </Stack>
            </Stack>
        </Flex>
    );

}

export default function UserProfileEdit(): JSX.Element {
    return <EditProfile />
}