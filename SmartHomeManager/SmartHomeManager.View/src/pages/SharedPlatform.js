import React, { useEffect, useState } from "react";
import { SharedProfileCardComponent } from "pages/profile/SharedCard";
import { Grid, GridItem, Box } from "@chakra-ui/react";
import user1 from "./profile/img/user1.png";

export default function SharedPlatform() {
    const [scenarios, updateScenarios] = useState([]);
    useEffect(() => {
        getScenarios()
        console.log(scenarios);
    },[])
  const getScenarios = async () => {
    const profileId = "22222222-2222-2222-2222-222222222222";
    await fetch(
      `https://localhost:7140/api/Scenarios/profileId?profileId=${profileId}`,
      {
        method: "GET",
        headers: {
          accept: "text/plain",
        },
      }
    ).then(async (response) => {
      const data = await response.json();

      if (response.ok) {
        updateScenarios(data);
        
      }
    });
    };
  //TODO: Call Controller
  return (
    <>
      <Grid
        templateRows="repeat(3, 1fr)"
        gap={3}
        paddingTop="3em"
        paddingRight="3em"
          >

              {/* NEED DO HAVENT DO PROPERLY, MAP ITEMS IN SCENARIO 
              {
                  scenarios.map((item) => (
                      <Stack key={item.isshareable} align={'center'}>
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
              }*/}
        <Box w="100%" m="0 auto" h="-moz-initial" bg="gray.50">
          <SharedProfileCardComponent
            ProfileName="Juleus"
            Temp="20.5"
            Light="Bright"
            TV="Off"
            ProfilePic={user1}
          />
        </Box>
        <Box w="100%" m="0 auto" h="moz-initial" bg="gray.50">
          <SharedProfileCardComponent
            ProfileName="KC"
            Temp="25"
            Light="Dim"
            TV="On"
            ProfilePic={user1}
          />
        </Box>
        <Box w="100%" m="0 auto" h="-moz-initial" bg="gray.50">
          <SharedProfileCardComponent
            ProfileName="Ryan"
            Temp="18 "
            Light="Ultra Bright"
            TV="On"
            ProfilePic={user1}
          />
        </Box>
      </Grid>
    </>
  );
}
