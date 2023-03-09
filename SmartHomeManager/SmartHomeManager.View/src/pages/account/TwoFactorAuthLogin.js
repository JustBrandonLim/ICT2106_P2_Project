import { InputRightElement } from '@chakra-ui/react';
import { React, useState, useEffect } from 'react';
import { Flex, Box, Stack, InputGroup, Button, Image, Input } from '@chakra-ui/react'
import { Fade, ScaleFade, Slide, SlideFade } from '@chakra-ui/react'
import { useNavigate, Link as RouterLink } from "react-router-dom";

export default function TwoFactorAuthLogin() {
    const navigate = useNavigate()

    const [accountId, updateAccountId] = useState(localStorage.getItem('accountId'))
    const [pinInput, updatePinInput] = useState("")
    const [validationMsg, updateValidationMsg] = useState("")
    const [isOpen, updateIsOpen] = useState(false)

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
                    navigate("/", { replace: true });
                }, 2000);
                
            }
            else {
                updateValidationMsg("Pin is incorrect!")
            }
        })
    }

    return (
        <>
            <Stack align={'center'} padding={'5%'} alignItems={'center'} >
                <InputGroup size='md' maxWidth='380px'>
                    <Input 
                        type="text" 
                        value={pinInput} 
                        placeholder="Enter your 2fa pin"
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
        </>
    );
}