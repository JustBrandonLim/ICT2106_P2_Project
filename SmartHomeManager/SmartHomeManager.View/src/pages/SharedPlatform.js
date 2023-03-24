import React, { useEffect, useState } from "react";
import { SharedProfileCardComponent } from "pages/profile/SharedCard";
import { Grid, Box } from "@chakra-ui/react";
import user1 from "./profile/img/user1.png";

export default function SharedPlatform() {
  const [scenarios, updateScenarios] = useState([]);
  const [scenarioData, setScenarioData] = useState({});

  useEffect(() => {
    const getScenarios = async () => {
      const profileId = "22222222-2222-2222-2222-222222222222";
      const response = await fetch(
        `https://localhost:7140/api/Scenarios/profileId?profileId=${profileId}`,
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

  return (
    <Grid
      templateRows="repeat(3, 1fr)"
      gap={3}
      paddingTop="3em"
      paddingRight="3em"
    >
      {scenarios.map((item) => {
        if (item.isShareable) {
          return (
            //Only display those profiles which are shareable to the community
            <Box key={item.profileId} w="100%" m="0 auto" bg="gray.50">
              <SharedProfileCardComponent
                ProfileName={item.profileId}
                SName={item.scenarioName}
                SId={item.scenarioId}
                IA={item.IsActive}
                ProfilePic={user1}
                Rules={scenarioData[item.scenarioId]?.map((item) => (
                  <div key={item.ruleName}>
                    {"\n\nRule Name: " + item.ruleName}
                    <br />
                    {"\nConfig Key: " + item.configurationKey}
                    <br />
                    {"\nConfiguration Value: " + item.configurationValue}
                    <br />
                    {"\nRule Id: " + item.ruleId}
                    <br />
                    <br />
                  </div>
                ))}
                // Use optional chaining to prevent errors
              />
            </Box>
          );
        }
      })}
    </Grid>
  );
}
