import { React, useState } from 'react';
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
    useToast,
    InputGroup,
    InputRightElement,
    Link,
    FormErrorMessage,
    FormHelperText,
    useColorModeValue,

} from '@chakra-ui/react';
import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';

export default function MyAccount() {
    const accountId = localStorage.getItem('accountId');

    //Navigation declaration
    const navigate = useNavigate()

    //Input declaration
    const [currentPasswordInput, updateCurrentPasswordInput] = useState("")
    const [newPasswordInput, updateNewPasswordInput] = useState("")
    const [confirmNewPasswordInput, updateConfirmNewPasswordInput] = useState("")

    //Boolean declaration for validation
    const [currentPasswordValid, updateCurrentPasswordValid] = useState(true)
    const [passwordValid, updatePasswordValid] = useState(true)
    const [confirmPasswordValid, updateConfirmPasswordValid] = useState(true)

    //Declaration for message
    const [currentPasswordMessage, updateCurrentPasswordMessage] = useState("")
    const [newPasswordMessage, updateNewPasswordMessage] = useState("")
    const [confirmNewPasswordMessage, updateNewConfirmPasswordMessage] = useState("")
    const toast = useToast();

    //Show password
    const [showCurrentPassword, setShowCurrentPassword] = useState(false);
    const [showNewPassword, setShowNewPassword] = useState(false);
    const [showCfmNewPassword, setShowCfmNewPassword] = useState(false);

    //Function to verify password
    const verifyPasswordInput = () => {

        if (currentPasswordInput.length == 0) {
            updateCurrentPasswordValid(true)
        }
        if (newPasswordInput.length == 0) {
            updatePasswordValid(true)
        }
        if (confirmNewPasswordInput.length == 0) {
            updateConfirmPasswordValid(true)
        }
        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
        var passwordFormat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        //Current Password
        if (currentPasswordInput.length != 0) {
            if (currentPasswordInput.length < 8) {
                updateCurrentPasswordMessage("Password should have a minimum of 8 characters")
                updateCurrentPasswordValid(false)
            } else if (passwordFormat.test(currentPasswordInput)) {
                updateCurrentPasswordMessage("Password is looking good")
                updateCurrentPasswordValid(true)
            } else {
                updateCurrentPasswordMessage("Password should have a minimum of 8 characters, at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character")
                updateCurrentPasswordValid(false)
            }
        }
        //new Password
        if (newPasswordInput.length != 0) {
            if (newPasswordInput.length < 8) {
                updateNewPasswordMessage("Password should have a minimum of 8 characters")
                updatePasswordValid(false)
            } else if (newPasswordInput == currentPasswordInput && passwordFormat.test(newPasswordInput)) {
                updateNewPasswordMessage("New Password is the same as current password, please use a new password")
                updatePasswordValid(false)
            } else if (passwordFormat.test(newPasswordInput)) {
                updateNewPasswordMessage("Password is looking good")
                updatePasswordValid(true)
            } else {
                updateNewPasswordMessage("New Password should have a minimum of 8 characters, at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character")
                updatePasswordValid(false)
            }
        }
        //new Confirm Password - Check if password match
        if (confirmNewPasswordInput.length != 0) {
            if (confirmNewPasswordInput.length < 8) {
                updateNewConfirmPasswordMessage("Password should have a minimum of 8 characters")
                updateConfirmPasswordValid(false)
            } else if (newPasswordInput == confirmNewPasswordInput && passwordFormat.test(confirmNewPasswordInput)) {
                updateNewConfirmPasswordMessage("New Password matched")
                updateConfirmPasswordValid(true)
            } else {
                updateNewConfirmPasswordMessage("New Password does not match or meet the required condition")
                updateConfirmPasswordValid(false)
            }
        }
    }

    //Function to update password
    const updatePassword = () => {
        //Check if current password is correct
        if (currentPasswordInput.length >= 8 && newPasswordInput.length >= 8 
            && confirmNewPasswordInput.length >= 8 && passwordValid && confirmPasswordValid) {
            //Obj for PasswordObj
            const accountPasswordObj = {
                "accountId": accountId, "password": currentPasswordInput
            }
            const accountNewPasswordObj = {
                "accountId": accountId, "password": newPasswordInput
            }

            //Check current password if matched DB
            fetch('https://localhost:7140/api/Accounts/passwordVerification', {
                method: 'POST',
                body: JSON.stringify(accountPasswordObj),
                headers: {
                    'Content-type': 'application/problem+json; charset=utf-8',
                },
            })
                .then(async response => {
                    /* Ok() - Password Match */
                    if (response.ok) {
                        updateCurrentPasswordValid(true)

                        //Update password
                        await fetch('https://localhost:7140/api/Accounts/updatePassword/', {
                            method: 'PUT',
                            body: JSON.stringify(accountNewPasswordObj),
                            headers: {
                                accept: 'text/plain',
                                'Content-Type': 'application/json',
                            },
                        })
                            .then(async response => {
                                /* Ok() - Password updated */
                                if (response.ok) {
                                    toast({
                                        title: "Success",
                                        description: "Password has been updated.",
                                        status: "success",
                                        duration: 9000,
                                        isClosable: true,
                                    });
                                    navigate("/myaccount", { replace: true });
                                } else {
                                    /*  BadRequest() - Password updated fail
                                    */
                                    toast({
                                        title: "Failed",
                                        description: "Password has fail to updated.",
                                        status: "error",
                                        duration: 9000,
                                        isClosable: true,
                                    });
                                    navigate("/myaccount", { replace: true });
                                }
                            })
                            .catch((err) => {
                                updateCurrentPasswordMessage(err.message);
                            });

                    } else {
                        /*  BadRequest() - Password Not Match
                        */
                        updateCurrentPasswordValid(false);
                        updateCurrentPasswordMessage("Current password incorrect")
                    }
                })
                .catch((err) => {
                    updateCurrentPasswordMessage(err.message)
                });


        }
    }

    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                <Stack align={'center'}>
                    <Heading fontSize={'4xl'}>Update Password</Heading>
                </Stack>
                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                    <Stack spacing={4}>
                        <FormControl id="currpassword" isRequired isInvalid={!currentPasswordValid}>
                            <FormLabel>Current password</FormLabel>
                            <InputGroup>
                                <Input type={showCurrentPassword ? 'text' : 'password'} minLength="8" value={currentPasswordInput} onChange={(e) => updateCurrentPasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                                <InputRightElement h={'full'}>
                                    <Button
                                        variant={'ghost'}
                                        onClick={() =>
                                            setShowCurrentPassword((showCurrentPassword) => !showCurrentPassword)
                                        }>
                                        {showCurrentPassword ? <ViewIcon /> : <ViewOffIcon />}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            {
                                currentPasswordValid ? (<FormHelperText>{currentPasswordMessage}</FormHelperText>) : (<FormErrorMessage>{currentPasswordMessage}</FormErrorMessage>)
                            }
                        </FormControl>
                        <FormControl id="newpassword" isRequired isInvalid={!passwordValid}>
                            <FormLabel>New password</FormLabel>
                            <InputGroup>
                                <Input type={showNewPassword ? 'text' : 'password'} minLength="8" value={newPasswordInput} onChange={(e) => updateNewPasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                                <InputRightElement h={'full'}>
                                    <Button
                                        variant={'ghost'}
                                        onClick={() =>
                                            setShowNewPassword((showNewPassword) => !showNewPassword)
                                        }>
                                        {showNewPassword ? <ViewIcon /> : <ViewOffIcon />}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            {
                                passwordValid ? (<FormHelperText>{newPasswordMessage}</FormHelperText>) : (<FormErrorMessage>{newPasswordMessage}</FormErrorMessage>)
                            }
                        </FormControl>
                        <FormControl id="cfmnewpassword" isRequired isInvalid={!confirmPasswordValid}>
                            <FormLabel>Confirm new password</FormLabel>
                            <InputGroup>
                                <Input type={showCfmNewPassword ? 'text' : 'password'} minLength="8" value={confirmNewPasswordInput} onChange={(e) => updateConfirmNewPasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />

                                <InputRightElement h={'full'}>
                                    <Button
                                        variant={'ghost'}
                                        onClick={() =>
                                            setShowCfmNewPassword((showCfmNewPassword) => !showCfmNewPassword)
                                        }>
                                        {showCfmNewPassword ? <ViewIcon /> : <ViewOffIcon />}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            {
                                confirmPasswordValid ? (<FormHelperText>{confirmNewPasswordMessage}</FormHelperText>) : (<FormErrorMessage>{confirmNewPasswordMessage}</FormErrorMessage>)
                            }
                        </FormControl>

                        <Stack spacing={4}>
                            <Button
                                onClick={() => updatePassword()}
                                bg={'yellow.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'yellow.500',
                                }}>
                                Change Password
                            </Button>

                        </Stack>
                    </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}