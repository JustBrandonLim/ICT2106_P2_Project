import React, { useEffect, useState } from "react";
import { SharedProfileCardComponent } from "pages/profile/SharedCard";
import { Grid, Box, Text, Flex } from "@chakra-ui/react";
import user1 from "./profile/img/user1.png";

export default function SharedPlatform() {
  const [scenarios, updateScenarios] = useState([]);
  const [scenarioData, setScenarioData] = useState({});

  useEffect(() => {
    const getScenarios = async () => {
      const profileId = "22222222-2222-2222-2222-222222222222";
      const response = await fetch(
        `https://localhost:7140/api/Scenarios/GetAllScenariosWithShareable`,
        {
          method: "GET",
          headers: {
            accept: "application/json",
          },
        }
      );
      const data = await response.json();
      if (response.ok) {
        updateScenarios(data);
      }
    };
    getScenarios();
  }, []);

  useEffect(() => {
    const fetchData = async (scenarioId) => {
      const response = await fetch(
        `https://localhost:7140/api/RulesControllerMock/GetByScenariosId/${scenarioId}`,
        {
          method: "GET",
          headers: {
            accept: "application/json",
          },
        }
      );
      const data = await response.json();
      if (response.ok) {
        setScenarioData((prevData) => ({
          ...prevData,
          [scenarioId]: data,
        }));
      }
    };

    scenarios.forEach((scenario) => {
      fetchData(scenario.scenarioId);
    });
  }, [scenarios]);

    const allScenariosNotShareable = scenarios.every((item) => !item.isShareable);

    return (
        <Grid
            templateRows="repeat(3, 1fr)"
            gap={3}
            paddingTop="3em"
            paddingRight="3em"
        >
            {scenarios.some((item) => item.isShareable) ? (
                scenarios.map((item) => {
                    if (item.isShareable) {
                        console.log(item);
                        return (
                            <Box key={item.scenarioId} w="100%" m="0 auto" bg="gray.50">
                                <SharedProfileCardComponent
                                    ProfileName={item.profileName}
                                    SName={item.scenarioName}
                                    SId={item.scenarioId}
                                    ProfilePic={user1}
                                    Rules={scenarioData[item.scenarioId]?.map((item) => (
                                        <div key={item.ruleName}>
                                            {"\n\nRule Name: " + item.ruleName}
                                            <br />
                                            {"\nConfig Key: " + item.configurationKey}
                                            <br />
                                            {"\nConfiguration Value: " + item.configurationValue}
                                            <br />
                                            {/* {"\nRule Id: " + item.ruleId}
                    <br /> */}
                                            <br />
                                        </div>
                                    ))}
                                />
                            </Box>
                        );
                    }
                })
            ) : (
                    <Flex justify="center" align="center">
                        <Text fontWeight="bold" fontSize="3xl">
                            No profiles have been shared yet.<br />
                            Please come back again later~
                        </Text>
                    </Flex>
            )}
        </Grid>
    );

}
