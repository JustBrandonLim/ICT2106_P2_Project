import { React, useState } from 'react';
import { useNavigate, useLocation } from "react-router-dom"
import {
    Button,
    Image,
    Box,
    useDisclosure
} from '@chakra-ui/react'


function ClickableImage({ imgSrc }) {
    const navigate = useNavigate();
    const [isClicked, setIsClicked] = useState(false);

    const { state } = useLocation()
    const profileName = state?.profileName
    const description = state?.Description


    const handleImageClick = (imgSrc) => {
        setIsClicked(true)
        navigate(`/edit-profile`, { state: { imgSrc, profileName, description } });
        console.log("im in clickable:" + imgSrc + profileName + description)

    }
    return (
        <Button boxSize='128px' marginLeft="10px" marginTop="10px"
            onClick={() => handleImageClick(imgSrc)}
            bg={isClicked ? "#E6ECFF" : "transparent"}        >
            <Image
                objectFit='cover'
                borderRadius='full'
                boxSize='128px'
                object-position="center"
                maxW={{ base: '80%', sm: '150px' }}
                src={imgSrc}
                alt='img'
            />
        </Button>
    );
}

export function ClickableImageComponent({ imgSrc }) {
    return <ClickableImage imgSrc={imgSrc} />
}