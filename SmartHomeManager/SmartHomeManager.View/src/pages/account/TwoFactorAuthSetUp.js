import { InputRightElement } from '@chakra-ui/react';
import { React, useState, useEffect } from 'react';
import { Flex, Box, Stack, InputGroup, Button, Image, Input } from '@chakra-ui/react'
import { Fade, ScaleFade, Slide, SlideFade } from '@chakra-ui/react'
import { useNavigate, Link as RouterLink } from "react-router-dom";


export default function TwoFactorAuthSetUp() {
    const navigate = useNavigate()

    const [accountId, updateAccountId] = useState(localStorage.getItem('accountId'))
    const [authDetails, updateAuthDetails] = useState([])
    const [pinInput, updatePinInput] = useState("")
    const [validationMsg, updateValidationMsg] = useState("")
    const [isOpen, updateIsOpen] = useState(false)
    

    useEffect(() => {
        const getQrDetails = async () => {
            await fetch(`https://localhost:7140/api/Accounts/security/get-qr-response?accountId=${accountId}`, {
                    method: 'GET',
                    headers: {
                        accept: 'text/plain'
                    },
                })
                .then(async response => {
                    const data = await response.json()

                    if (response.ok) {
                        updateAuthDetails(data)
                    }
                })
        }
        getQrDetails()
    }, [accountId]);

    const validatePin = async () => {
        updateIsOpen(true)
        const validatePinObj = {
            "authenticationCode": accountId, "pin": pinInput
        }
        await fetch('https://localhost:7140/api/Accounts/security/validate-2fa-pin', {
            method: 'POST',
            body: JSON.stringify(validatePinObj),
            headers: {
                'Content-type': 'application/problem+json; charset=utf-8',
            },
        })
        .then(async response => {
            const msg = await response.text();
            if (response.ok) {
                updateValidationMsg("Pin verified!")
                setTimeout(() => { 
                    fetch(`https://localhost:7140/api/Accounts/security/update-2fa-flag?accountId=${accountId}&twoFactorFlag=true`, 
                    {
                        method: 'PUT',
                        headers: {
                            'Content-type': 'application/problem+json; charset=utf-8',
                        },
                    })
                    var flag = "true"
                    localStorage.setItem('twoFactorFlag', JSON.parse(flag) === true);
                    navigate("/two-factor-auth-setup-success", { replace: true });
                }, 2000);
                
            }
            else {
                updateValidationMsg("Pin is incorrect!")
            }
        })
    }

    return (
        <>
            {
                authDetails.map((item) => (
                    <Stack key={item.authenticationCode} align={'center'}>
                        <div>
                            Scan the QR code with your mobile device!
                        </div>
                        <Image 
                            src={item.authenticationBarCodeImage}
                            boxSize='250px'
                            >
                        </Image>
                        <div>
                            Your Manual Setup Key:
                        </div>
                        <div>
                            {item.authenticationManualCode}
                        </div>
                    </Stack>
                ))
            }

            {
                <Stack align={'center'} padding={'5%'} alignItems={'center'} >
                    <InputGroup size='md' maxWidth='380px'>
                        <Input 
                            type="text" 
                            value={pinInput} 
                            onChange={(e) => updatePinInput(e.target.value)} 
                            maxW='200px'
                        />

                        <InputRightElement width='200px'>
                            <Button
                                onClick={() => validatePin()}
                                bg={'green.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'green.500',
                                }}>
                                Validate Pin
                            </Button>
                        </InputRightElement> 
                    </InputGroup>

                    <ScaleFade initialScale={0.7} in={isOpen}>
                        <Box fontSize={'xl'}>
                            {validationMsg}
                        </Box>
                    </ScaleFade>
                </Stack>
            }
        </>
    );
}

