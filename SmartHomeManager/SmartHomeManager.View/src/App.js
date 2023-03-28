import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/profile/Profiles";
import Director from "./pages/Director";
import Backup from "./pages/Backup";
import Intruder from "./pages/Intruder";
import Configuration from "./pages/Configuration";
import EnergyProfile from "./pages/EnergyProfile";
import Scenario from "./pages/Scenario";
import SchRule from "pages/SchRule";
import ActionRule from "pages/ActionRule";
import Rooms from "./pages/Rooms";
import { Container } from "@chakra-ui/react";
import Register from "./pages/account/Register";
import ForgetPassword from "./pages/account/ForgetPassword";
import ChangePassword from "./pages/account/ChangePassword";
import Login from "./pages/account/Login";
import RegisterOK from "./pages/account/RegisterOK";
import OnboardDevice from "./pages/account/OnboardDevice";
import MyAccount from "./pages/account/Setting";
import UserProfileEdit from "./pages/profile/EditProfile";
import AddProfile from "./pages/profile/AddProfile";
import ProfileSelected from "./pages/profile/ProfileSelected";
import RegisterDevice from "./pages/RegisterDevice";
import SelectNearbyDevice from "pages/SelectNearbyDevice";
import DeviceConfig from "./pages/DeviceConfig";
import Report from "pages/Analytics";
import TwoFactorAuthSetUp from "./pages/account/TwoFactorAuthSetUp";
import TwoFactorAuthSetUpSuccess from "./pages/account/TwoFactorAuthSetUpSuccess";
import TwoFactorAuthLogin from "./pages/account/TwoFactorAuthLogin";
import SharedPlatform from "pages/SharedPlatform";
import ProfileLanding from "./pages/profile/ProfileLanding";
import RegisterDevice from "./pages/device/register-device/RegisterDevice";
import SelectDevice from "pages/device/register-device/SelectDevice";
import DeviceConfig from "./pages/DeviceConfig";
import Report from "pages/Analytics";
import Room2D from "pages/Room2D";
import ManageDevices from "pages/device/manage-device/ManageDevices";
import ManageDeviceConfiguration from "pages/device/manage-device/ManageDeviceConfiguration";
import ManageDeviceSettings from "pages/device/manage-device/ManageDeviceSettings";
import "/node_modules/react-grid-layout/css/styles.css";
import "/node_modules/react-resizable/css/styles.css";
import Store from "pages/Store"

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Container maxW={"6xl"} py={4}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/devices" element={<Devices />} />

            <Route path="/rooms" element={<Rooms />} />
            <Route path="/room2D" element={<Room2D />} />
						<Route path="/store" element={<Store />} />
            <Route path="/profiles" element={<Profiles />} />
            <Route path="/forgetpw" element={<ForgetPassword />} />
            <Route path="/changepw" element={<ChangePassword />} />
            <Route path="/login" element={<Login />} />
            <Route path="/myaccount" element={<MyAccount />} />
            <Route path="/account-created" element={<RegisterOK />} />
            <Route path="/edit-profile" element={<UserProfileEdit />} />
            <Route path="/add-profile" element={<AddProfile />} />
            <Route path="/profile-selected" element={<ProfileSelected />} />
            <Route path="/register" element={<Register />} />
            <Route path="/two-factor-auth-setup" element={<TwoFactorAuthSetUp />} />
            <Route path="/two-factor-auth-setup-success" element={<TwoFactorAuthSetUpSuccess />} />
            <Route path="/two-factor-auth-login" element={<TwoFactorAuthLogin />} />
            <Route path="/sharedplatform" element={<SharedPlatform />} />
            <Route path="/onboard-devices" element={<OnboardDevice />} />
            <Route path="/selectnearbydevice" element={<SelectNearbyDevice />} />
            <Route path="/selectnearbydevice" element={<SelectDevice />} />
            <Route path="/registerdevice" element={<RegisterDevice />} />
            <Route path="/managedevices" element={<ManageDevices />} />
            <Route path="/managedeviceconfiguration" element={<ManageDeviceConfiguration />} />
            <Route path="/managedevicesettings" element={<ManageDeviceSettings />} />

            <Route path="/director" element={<Director />} />
            <Route path="/backup" element={<Backup />} />
            <Route path="/intruder" element={<Intruder />} />
            <Route path="/configuration" element={<Configuration />} />
            <Route path="/energyProfile" element={<EnergyProfile />} />
            <Route path="/scenario" element={<Scenario />} />
            <Route path="/scenario/create/action-rule" element={<ActionRule />} />
            <Route path="/scenario/create/time-rule" element={<SchRule />} />
            <Route path="/scenario/edit/:id" element={<SchRule />} />
            <Route path="/config" element={<DeviceConfig />} />
            <Route path="/analytics" element={<Report />} />
          </Routes>
        </Container>
      </Router>
    </>
  );
}
export default App
