import { React, useState, useEffect } from 'react';
import { useNavigate, Link as RouterLink } from "react-router-dom";
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    FormErrorMessage,
    Input,
    Checkbox,
    Stack,
    Link,
    Button,
    Heading,
    Text,
    useColorModeValue,
} from '@chakra-ui/react';

export default function Login() {

    //Navigation declaration
    const navigate = useNavigate()

    //Email input + validation
    const [emailInput, updateEmailInput] = useState("")
    const [emailValid, updateEmailValid] = useState(true)
    const [passwordInput, updatePasswordInput] = useState("")
    const [errorMsg, updateErrorMsg] = useState("")
    const [errorStatus, updateErrorStatus] = useState(false)
    const [accountDetails, updateAccountDetails] = useState([])
    useEffect(() => {
        const checkAccountDetails = () => {
            // check if 2fa flag enabled first
            // if enabled, navigate to /two-factor-auth-login
            // // if not, navigate to /

            if (accountDetails.length != 0) {
                setAccountDetails()
                
                if(accountDetails['twoFactorFlag'])
                    navigate("/two-factor-auth-login", { replace: true })
                else {
                    // navigate("/", { replace: true }) 
                    navigate("/onboard-devices", { replace: true })  
                    location.reload() 

                }    
            }        
        }
        checkAccountDetails()
    }, [accountDetails]);

    //Function to verify email
    const checkEmailInput = (emailInput) => {
        if (emailInput.length == 0) {
            updateEmailValid(true)
        } else {
            var mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            updateEmailValid(mailFormat.test(emailInput))
        }
    }

    //Function to submit login form
    const submitLoginForm = () => {
        if (emailValid && passwordInput.length >= 8) {
            const loginAccountObj = {
                "email": emailInput, "password": passwordInput
            }
            fetch('https://localhost:7140/api/Accounts/login', {
                method: 'POST',
                body: JSON.stringify(loginAccountObj),
                headers: {
                    'Content-type': 'application/problem+json; charset=utf-8',
                },
            })
            .then(async response => {
                const msg = await response.json();
                /* Ok(1) - Login Successful */
                if (response.ok) {
                    updateErrorStatus(false);
                    updateAccountDetails(msg)
                } else {
                    /*  BadRequest(1) - Login Unsuccessful, wrong password
                    *   BadRequest(2) - Login Unsuccessful, account does not exist
                    */
                    updateErrorStatus(true);
                    updateErrorMsg("Incorrect email or password");
                }
            })
            .catch((err) => {
                updateErrorMsg(err.message);
            });
        } else {
            updateErrorStatus(true);
            updateErrorMsg("Incorrect email or password");
        }
    }

    const setAccountDetails = () => {
        if (accountDetails.length != 0) {
            localStorage.setItem('accountId', accountDetails['accountId']);
            localStorage.setItem('email', accountDetails['email']);
            localStorage.setItem('username', accountDetails['username']);
            localStorage.setItem('twoFactorFlag', accountDetails['twoFactorFlag']);
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
                    <Heading fontSize={'4xl'}>Sign in to your account</Heading>
                </Stack>
                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                    <Stack spacing={4}>

                        {
                            errorStatus ? <Heading color={'red'} textAlign={'center'} fontSize={'1xl'}>{errorMsg}</Heading> : ""
                        }
                        <FormControl id="email" isInvalid={!emailValid}>
                            <FormLabel>Email address</FormLabel>
                            <Input type="email" onChange={(e) => { updateEmailInput(e.target.value); checkEmailInput(e.target.value) }} />
                            {
                                (emailValid && emailInput.length > 0) ? "" : (<FormErrorMessage>Email is invalid!</FormErrorMessage>)
                            }
                        </FormControl>
                        <FormControl id="password">
                            <FormLabel>Password</FormLabel>
                            <Input type="password" value={passwordInput} onChange={(e) => updatePasswordInput(e.target.value)} />
                        </FormControl>
                        <Stack spacing={10}>
                            <Stack
                                direction={{ base: 'column', sm: 'row' }}
                                align={'start'}
                                justify={'space-between'}>
                                <Checkbox>Remember me</Checkbox>
                                <Link color={'blue.400'}
                                    as={RouterLink}
                                    to="/forgetpw">Forgot password?</Link>
                            </Stack>
                            <Button
                                onClick={() => submitLoginForm()}
                                type='submit'
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Sign in
                            </Button>
                        </Stack>
                        <Stack pt={6}>
                            <Text align={'center'}>
                                Don&#39;t have an account? <Link color={'blue.400'}
                                    as={RouterLink}
                                    to="/register">Sign up</Link>
                            </Text>
                        </Stack>
                    </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}
