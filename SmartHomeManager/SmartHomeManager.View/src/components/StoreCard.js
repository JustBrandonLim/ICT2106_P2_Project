/* eslint-disable react/prop-types */
import { React, useState } from 'react'
import {
	Flex,
	Box,
	Image,
	useColorModeValue,
	Button,
	Spacer,
} from '@chakra-ui/react'

function PriceButton({ price }) {
	const [isHovered, setIsHovered] = useState(false)

	const handleMouseEnter = () => {
		setIsHovered(true)
	}

	const handleMouseLeave = () => {
		setIsHovered(false)
	}

	return (
		<Button
			colorScheme="blue"
			width={'full'}
			transition="all 0.3s"
			bg={isHovered ? 'blue.500' : 'blue.700'}
			color={isHovered ? 'white' : 'blue.300'}
			onMouseEnter={handleMouseEnter}
			onMouseLeave={handleMouseLeave}
		>
			{isHovered ? 'Buy now!' : `$ ${price.toFixed(2)}`}
		</Button>
	)
}

function StoreCard({ imageURL, name, price, description }) {
	return (
		<Flex w="full" alignItems="center" justifyContent="center" h="auto">
			<Flex
				bg={useColorModeValue('white', 'gray.800')}
				maxW="sm"
				h="auto"
				borderWidth="1px"
				direction={'column'}
				rounded="lg"
				shadow="lg"
			>
				<Flex w="100%" h="160px">
					<Image
						src={imageURL}
						alt={`Picture of ${name}`}
						roundedTop="lg"
						w="100%"
						h="100%"
						objectFit="cover"
					/>
				</Flex>

				<Flex direction={{ base: 'row', md: 'column' }} p="4" h={'full'}>
					<Flex mt="1" justifyContent="space-between" alignContent="center">
						<Box fontSize="lg" fontWeight="semibold">
							{name}
						</Box>
					</Flex>
					<Flex
						mt="2"
						fontSize="sm"
						color={useColorModeValue('gray.600', 'gray.400')}
						height="120px"
					>
						{description}
					</Flex>
					<Spacer />
					<Flex justifyContent="space-between" alignContent="center">
						<PriceButton price={price} />
					</Flex>
				</Flex>
			</Flex>
		</Flex>
	)
}

export default StoreCard
