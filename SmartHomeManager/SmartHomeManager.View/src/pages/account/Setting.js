import { React, useState, useRef, useEffect } from 'react';
import { useNavigate, Link as RouterLink } from "react-router-dom";

import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Stack,
    Button,
    Input,
    Heading,
    Text,
    useColorModeValue,
    AlertDialog,
    AlertDialogBody,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogContent,
    AlertDialogOverlay,
    useDisclosure
} from '@chakra-ui/react';

import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import { isDisabled } from '@testing-library/user-event/dist/utils';

export default function MyAccount() {
    const accountId = localStorage.getItem('accountId');
    const [twoFactorFlag, updateTwoFactorFlag] = useState(JSON.parse(localStorage.getItem('twoFactorFlag')))
    const [username, updateUsername] = useState("")
    const [email, updateEmail] = useState("")
    const [timezone, updateTimezone] = useState(0)
    const [address, updateAddress] = useState("")

    const { isOpen, onOpen, onClose } = useDisclosure()
    const cancelRef = useRef()

    const navigate = useNavigate()

    const handleOnOpen = () => {
        console.log(twoFactorFlag)
        !twoFactorFlag ? navigate("/two-factor-auth-setup", { replace: true }) : ""
    }

    const handleOnClose = async () => {
        twoFactorFlag ? await fetch(`https://localhost:7140/api/Accounts/security/update-2fa-flag?accountId=${accountId}&twoFactorFlag=false`, 
        {
            method: 'PUT',
            headers: {
                'Content-type': 'application/problem+json; charset=utf-8',
            },
        }).then(res => {
            // console.log(res)
            if (res.ok)
                localStorage.setItem('twoFactorFlag', JSON.parse(false) === "false")
            window.location.reload()
        }) : ""
    }

    //Retrieve account information
    const onLoad = () =>{
        fetch('https://localhost:7140/api/Accounts/'+accountId, {
            method: 'GET',
            headers: {
                accept: 'text/plain',
            },
        })
        .then(async response => {
            const msg = await response.json();
            if (response.ok) {
                updateUsername(msg["username"])
                updateEmail(msg["email"])
                updateTimezone(parseInt(msg["timezone"]))
                updateAddress(msg["address"])
            } 
        })
        .catch((err) => {
        });
    }
    onLoad()
    
    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                <Stack align={'center'}>
                    <Heading fontSize={'4xl'}>Update Account Details</Heading>
                </Stack>
                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                    <Stack spacing={4}>
                        <FormControl id="username">
                            <FormLabel>Username:</FormLabel>
                            <Text>{username}</Text>
                        </FormControl>

                        <FormControl id="email">
                            <FormLabel>Email address:</FormLabel>
                            <Text>{email}</Text>
                        </FormControl>
                        
                        <FormControl id="timezone">
                            <FormLabel>Timezone:</FormLabel>
                            <Text>{ timezone>0 ? "GMT +"+timezone : "GMT "+timezone}</Text>
                        </FormControl>

                        <FormControl id="address">
                            <FormLabel>Address:</FormLabel>
                            <Text>{address}</Text>
                        </FormControl>

                        <Stack spacing={4}>
                            <Button
                                as={RouterLink}
                                to="/changepw"
                                bg={'yellow.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'yellow.500',
                                }}>
                                Change Password
                            </Button>
                            
                                <Stack onClick={handleOnOpen}>
                                    <Button 
                                    onClick={onOpen}
                                    bg={'green.400'}
                                    color={'white'}
                                    _hover={{
                                        bg: 'green.500',
                                    }}>
                                        { !twoFactorFlag ? "Enable 2FA" : "Disable 2FA"}
                                    </Button>
                                </Stack>
                        </Stack>
                    </Stack>
                </Box>
            </Stack>

            <AlertDialog
                isOpen={isOpen}
                leastDestructiveRef={cancelRef}
                onClose={onClose}
              >
                <AlertDialogOverlay>
                  <AlertDialogContent>
                    <AlertDialogHeader fontSize='lg' fontWeight='bold'>
                      Disable 2FA
                    </AlertDialogHeader>
        
                    <AlertDialogBody>
                      Are you sure?
                    </AlertDialogBody>
        
                    <AlertDialogFooter>
                      <Button ref={cancelRef} onClick={onClose}>
                        Cancel
                      </Button>
                      
                    <Stack onClick={handleOnClose}>         
                        <Button colorScheme='red' onClick={onClose} ml={3}>
                            Disable
                        </Button>
                    </Stack>       
                      
                    </AlertDialogFooter>
                  </AlertDialogContent>
                </AlertDialogOverlay>
              </AlertDialog>
        </Flex>
    );

    
}