import React, { useState, useEffect } from 'react'
import {
	AddIcon,
	CheckIcon,
	EditIcon,
	MinusIcon,
	PlusSquareIcon,
} from '@chakra-ui/icons'
import {
	Button,
	ButtonGroup,
	Center,
	Container,
	Flex,
	Heading,
	IconButton,
	Input,
	Modal,
	ModalBody,
	ModalCloseButton,
	ModalContent,
	ModalFooter,
	ModalHeader,
	ModalOverlay,
	Select,
	Stack,
	useColorModeValue,
} from '@chakra-ui/react'
import GridLayout from 'react-grid-layout'
import { MdPowerSettingsNew } from 'react-icons/md'
import axios from 'axios'

export default function Room2D() {
	// take in the data object from the API
	const [data, setData] = useState([])
	// get the coordinates and put into layout
	const [layout, setLayout] = useState([])
	// get the rooms and put into rooms
	const [rooms, setRooms] = useState([])
	// get the devices and put into devices
	const [devices, setDevices] = useState([])
	const [isDialogOpen, setIsDialogOpen] = useState(false)
	const [selectedRoom, setSelectedRoom] = useState('')
	const [devicesState, setDevicesState] = useState()
	const [isEditing, setIsEditing] = useState(false)

	const handleAddRoom = () => {
		setIsDialogOpen(true)
	}

	const handleDialogClose = () => {
		setIsDialogOpen(false)
		setRoomName('')
	}

	const handleDialogSubmit = () => {
		const newData = { ...data }

		const newRoomData = newData.roomGrids.find(
			(room) => room.roomId === selectedRoom
		)
		const newRoom = {
			roomId: selectedRoom, // Use the roomId from your data
			roomName: newRoomData.roomName,
			x: -1, // Set x, y, w, and h to -1 initially
			y: -1,
			w: -1,
			h: -1,
		}
		setRooms([...rooms, newRoom.roomName])
		onAddRoom(newRoom)
		setIsDialogOpen(false)
	}

	const assignRoomCoordinates = () => {
		// Find the first available position on the grid
		let x = 0
		let y = 0
		let found = false

		while (!found) {
			const existingRoom = layout.find((room) => room.x === x && room.y === y)
			if (!existingRoom) {
				found = true
			} else {
				x++
				if (x >= 8) {
					x = 0
					y++
				}
			}
		}

		return { x, y, w: 2, h: 2 } // You can adjust the default width and height here
	}

	const onAddRoom = (room) => {
		const coordinates = assignRoomCoordinates()

		const newRoom = {
			i: `${room.roomId}`,
			x: coordinates.x,
			y: coordinates.y,
			w: coordinates.w,
			h: coordinates.h,
			name: room.roomName,
		}
		setLayout([...layout, newRoom])
	}

	const removeRoom = (roomName) => {
		const roomIndex = rooms.indexOf(roomName)
		if (roomIndex === -1) {
			return
		}

		setSelectedRoom('')
		const newRooms = [...rooms]
		newRooms.splice(roomIndex, 1)
		setRooms(newRooms)

		const newLayout = [...layout]
		const roomToRemove = newLayout[roomIndex]
		roomToRemove.x = -1
		roomToRemove.y = -1
		roomToRemove.w = -1
		roomToRemove.h = -1

		// Update the room coordinates in the backend
		updateRoomCoordinates(newLayout)
	}

	const handleDeviceClick = (roomName, deviceId) => {
		setDevicesState((prevState) => {
			const newState = { ...prevState }
			console.log('line: 136 newState', newState)
			const deviceIndex = newState[roomName].findIndex(
				(device) => device.deviceId === deviceId
			)
			if (deviceIndex !== -1) {
				newState[roomName][deviceIndex].isOn =
					!newState[roomName][deviceIndex].isOn
			}
			console.log('line: 144 newState after', newState)
			return newState
		})
	}

	const fetchRoomCoordinates = async () => {
		const accountId = '11111111-1111-1111-1111-111111111111'
		const response = await fetch(
			`https://localhost:7140/api/TwoDHomes/GetAllRoomGridsRelatedToAccount/${accountId}`,
			{
				method: 'GET',
				headers: { accept: 'text/plain' },
			}
		)

		if (response.ok) {
			const data = await response.json()
			console.log('data received', data)
			setData(data)

			const newLayout =
				data.roomGrids
					.filter(
						(room) =>
							room.x !== -1 && room.y !== -1 && room.w !== -1 && room.h !== -1
					)
					.map((room) => ({
						i: room.roomId,
						x: room.x,
						y: room.y,
						w: room.w,
						h: room.h,
						name: room.roomName,
						deviceControls: room.deviceControls,
					})) ?? []

			setLayout(newLayout)

			const newRooms = data.roomGrids.map((room) => room.roomName)
			setRooms(newRooms)

			const newDevicesState = {}
			data.roomGrids.forEach((room) => {
				newDevicesState[room.roomName] = room.deviceControls.map((device) => ({
					deviceId: device.deviceId,
					deviceName: device.deviceName,
					isOn: device.isOn,
					isLoading: false,
				}))
			})
			console.log('newDevicesState', newDevicesState)
			setDevicesState(newDevicesState)
		} else {
			console.error('Failed to fetch room coordinates')
		}
	}

	useEffect(() => {
		fetchRoomCoordinates()
	}, [])

	// Update updateRoomCoordinates function
	const updateRoomCoordinates = async (updatedLayout) => {
		const accountId = '11111111-1111-1111-1111-111111111111'

		// Unwrap data object
		const newData = { ...data }

		// Update roomGrids with new coordinates
		newData.roomGrids = updatedLayout
			.filter((roomGrid) => roomGrid.x !== -1) // Only include rooms with different coordinates than -1
			.map((roomGrid) => {
				// Update current roomGrids with new coordinates only
				const newRoomGrid = newData.roomGrids.find(
					(room) => room.roomId === roomGrid.i
				)
				newRoomGrid.x = roomGrid.x
				newRoomGrid.y = roomGrid.y
				newRoomGrid.w = roomGrid.w
				newRoomGrid.h = roomGrid.h
				return newRoomGrid
			})

		try {
			axios
				.put(
					'https://localhost:7140/api/TwoDHomes/UpdateAllRoomGridsRelatedToAccount/11111111-1111-1111-1111-111111111111',
					newData
				)
				.then((response) => {
					console.log('Success:', response)
					fetchRoomCoordinates()
				})
				.catch((error) => {
					console.error('Error:', error)
				})
		} catch (error) {
			console.error('Error updating room coordinates:', error)
		}
	}
	return (
		<>
			<Flex verticalAlign={'center'} mb="2" gap={'2'}>
				<Heading>2D Room View</Heading>
				<Button
					leftIcon={<AddIcon />}
					onClick={() => {
						handleAddRoom()
					}}
					variant="outline"
					colorScheme={'blue'}
				>
					Add Room
				</Button>
				<Button
					onClick={() => {
						if (isEditing) {
							updateRoomCoordinates(layout)
						}
						setIsEditing(!isEditing)
					}}
					colorScheme={isEditing ? 'green' : 'blue'}
					leftIcon={isEditing ? <CheckIcon /> : <EditIcon />}
				>
					{isEditing ? 'Done' : 'Edit'}
				</Button>
				<ButtonGroup display={isEditing ? 'flex' : 'none'}>
					<Select
						placeholder="Select room to delete"
						w="auto"
						value={selectedRoom}
						onChange={(e) => setSelectedRoom(e.target.value)}
					>
						{layout.map((layoutItem, index) => (
							<option key={index}>{layoutItem.name}</option>
						))}
					</Select>
					<Button
						ms="2"
						leftIcon={<MinusIcon />}
						colorScheme={selectedRoom ? 'red' : ''}
						onClick={() => {
							removeRoom(selectedRoom)
						}}
					>
						Remove Room
					</Button>
				</ButtonGroup>
			</Flex>
			<GridLayout
				className="layout"
				layout={layout}
				cols={8}
				rowHeight={40}
				width={1200}
				isDraggable={isEditing}
				isResizable={isEditing}
				onLayoutChange={(newLayout) => {
					setLayout(newLayout)
					updateRoomCoordinates(newLayout)

					if (!isEditing) {
						//add a delay of 500ms to prevent multiple calls to updateRoomCoordinates
						// setTimeout(() => {
						// updateRoomCoordinates(newLayout)
						// supposed to be here if anyt breaks LOL
						// }, 100)
					}
				}}
			>
				{layout.map((layoutItem, index) => (
					<Flex
						key={layoutItem.i}
						bg="gray.700"
						h="full"
						p="4"
						borderRadius={'md'}
						direction={'column'}
					>
						<Heading color={'gray.200'} size={'sm'}>
							{layoutItem.name}
						</Heading>

						<Stack spacing={4} direction="row" align="center" py="2">
							{devicesState[layoutItem.name] &&
								devicesState[layoutItem.name].map((device) => (
									<Button
										key={device.deviceId}
										colorScheme={device.isOn ? 'teal' : 'gray'}
										size="xs"
										leftIcon={<MdPowerSettingsNew />}
										onClick={() => {
											handleDeviceClick(layoutItem.name, device.deviceId)
										}}
									>
										{device.deviceName}
									</Button>
								))}
						</Stack>
					</Flex>
				))}
			</GridLayout>
			<Modal isOpen={isDialogOpen} onClose={handleDialogClose}>
				<ModalOverlay />
				<ModalContent>
					<ModalHeader>Add Room</ModalHeader>
					<ModalCloseButton />
					<ModalBody>
						{data?.roomGrids?.some((room) => room.x === -1) ? (
							<Select
								onChange={(e) => {
									setSelectedRoom(e.target.value)
								}}
								placeholder="Select room to add"
							>
								{data?.roomGrids
									?.filter((room) => room.x === -1)
									?.map((room) => (
										<option key={room.roomId} value={room.roomId}>
											{room.roomName}
										</option>
									))}
							</Select>
						) : (
							<Heading textAlign={'center'} size="lg">
								No room available to add to 2D View
							</Heading>
						)}
					</ModalBody>
					<ModalFooter>
						<Button
							colorScheme={'green'}
							onClick={() => {
								handleDialogSubmit(selectedRoom)
							}}
							// hide button if no room is available to add
							hidden={!data?.roomGrids?.some((room) => room.x === -1)}
						>
							Add Room
						</Button>
					</ModalFooter>
				</ModalContent>
			</Modal>
		</>
	)
}
