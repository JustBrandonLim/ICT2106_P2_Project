import { InputRightElement } from '@chakra-ui/react';
import { React, useState, useEffect } from 'react';
import { InputGroup, Button, Image, Input } from '@chakra-ui/react'


export default function TwoFactorAuth() {
    const [accountId, updateAccountId] = useState(JSON.parse(localStorage.getItem('accountId')))
    const [authDetails, updateAuthDetails] = useState([])
    const [pinInput, updatePinInput] = useState("")

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
                console.log("Pin validated")
            }
            else {
                console.log("Pin is wrong")
            }
        })
    }

    return (
        <>
            {
                authDetails.map((item) => (
                    <div key={item.authenticationCode}>
                        <Image 
                            src={item.authenticationBarCodeImage}
                            boxSize='300px'
                            >
                        </Image>
                        Your Secret Key: {item.authenticationManualCode}
                    </div>
                ))
            }

            {
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
            }
        </>
    );
}

