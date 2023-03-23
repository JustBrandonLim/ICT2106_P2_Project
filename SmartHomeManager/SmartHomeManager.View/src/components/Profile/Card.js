import { React, useState } from "react";
import { Card, CardHeader, CardBody, CardFooter, Stack, Heading, Text, Button, Image } from '@chakra-ui/react'
import { Link, useNavigate } from "react-router-dom"
import { css } from '@emotion/react';

function ProfileCard({ profileName, imgSrc, Description, profileId }) {

    const navigate = useNavigate();

    const cardHoverStyle = css`&:hover {
    cursor: pointer;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    transform: translateY(-2px);
    }`;
    const handleEditProfileClick = (profileName, imgSrc, Description, profileId) => {
        // Handle edit profile click
        navigate(`/edit-profile`, { state: { profileId, profileName, imgSrc, Description, profileId } });
    }

    const handleAddProfileClick = () => {
        // Handle add profile click
        navigate(`/add-profile`)
    }

    function handleCardClick(profileId) {
        navigate(`/profile-selected`, { state: { profileId } })
    }
    return (


        <Card
            css={cardHoverStyle}
            direction={{ base: 'column', sm: 'row' }}
            size="sm"
            h="250px"
            overflow='hidden' >
            <Button onClick={() => handleCardClick(profileId)}
                bg={"transparent"}
                boxSize='120px' marginLeft="10px" marginTop="10px"
                borderRadius="50%"
                object-position="center">
                <Image
                    objectFit='cover'
                    borderRadius='full'
                    boxSize='128px'
                    object-position="center"
                    marginTop="10px"
                    marginLeft="10px"
                    maxW={{ base: '80%', sm: '150px' }}
                    src={imgSrc}
                    alt='img'

                />
            </Button>

            <Stack>
                <CardBody>
                    <Heading size='md' margin="10px">{profileName}</Heading>

                    <Text py='2' marginRight="10px" marginLeft="10px">
                        {Description}
                    </Text>
                </CardBody>
                {profileName !== "Add Profile" ? (
                    <CardFooter>
                        <Button
                            variant='solid'
                            colorScheme='blue'
                            onClick={() => handleEditProfileClick(profileName, imgSrc, Description, profileId)}
                            marginLeft="10px">
                            Edit Profile
                        </Button>
                    </CardFooter>
                ) : (
                    <CardFooter>
                        <Button variant='solid' colorScheme='blue' onClick={handleAddProfileClick} margin="10px">
                            Add Profile
                        </Button>
                    </CardFooter>
                )}
            </Stack>
        </Card>
    )
}

export function CardComponent({ imgSrc, profileName, description, profileId }) {
    return <ProfileCard profileName={profileName} imgSrc={imgSrc} Description={description} profileId={profileId} />
}