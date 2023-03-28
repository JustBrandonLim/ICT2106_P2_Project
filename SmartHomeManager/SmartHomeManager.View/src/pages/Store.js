import { Grid, GridItem, Heading } from '@chakra-ui/react'
import React from 'react'
import StoreCard from 'components/StoreCard'

export default function Store() {
	const cardData = [
		{
			isNew: true,
			imageURL:
				'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSsbhp7heO7byDLWEkolkfxjrlp_s86TrXIog&usqp=CAU',
			name: 'Fan',
			description:
				'Equipped with Wi-Fi connectivity, voice control, and mobile app control for remote operation.',
			price: 129.99,
		},
		{
			isNew: false,
			imageURL:
				'https://cdn.shopify.com/s/files/1/2393/8647/products/5AT1S3-WEN0.jpg?v=1606809887',
			name: 'Smart Light ',
			description:
				'Remotely controllable and customizable light through a smartphone, tablet or voice assistant.',
			price: 7.99,
		},
		{
			isNew: false,
			imageURL:
				'https://klivago.com/media/image/product/1131/md/mitsubishi-air-conditioner-r32-wall-unit-premium-msz-ef50vgb-50-kw-i-18000-btu-black~2.jpg',
			name: 'Aircon',
			description:
				'Connected and controllable air conditioning system that can be managed through a mobile app or voice assistants.',
			price: 1299.99,
		},
		{
			isNew: false,
			imageURL:
				'https://i5.walmartimages.com/asr/3b0fd42b-f064-45a2-8cee-92a06af63f1f.024fefeb68201fe69680b37142b327a1.jpeg',
			name: 'Smart TV',
			description:
				'Internet-enabled TV that offers video streaming services, web browsing, and social media integration for access to various online content.',
			price: 1299.99,
		},
	]
	return (
		<>
			<Heading mb="2">Store</Heading>

			<Grid templateColumns="repeat(4, 1fr)" gap={4}>
				{cardData.map((card) => (
					<GridItem key={card.name} w="100%" h="320px">
						<StoreCard
							isNew={card.isNew}
							imageURL={card.imageURL}
							name={card.name}
							price={card.price}
							description={card.description}
						/>
					</GridItem>
				))}
			</Grid>
		</>
	)
}
