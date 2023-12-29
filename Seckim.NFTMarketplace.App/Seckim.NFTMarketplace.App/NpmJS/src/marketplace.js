import { ethers } from "ethers";
import MarketplaceJson from '../../NpmJS/src/contracts/NFTMarketplace.json'
import NftJson from '../../NpmJS/src/contracts/NFT.json'
import { MKTAddress, NFTAddress, AdminAddress } from '../../NpmJS/src/constants'

const provider = new ethers.BrowserProvider(window.ethereum);

const balanceInEth = (balance) => {
  const array = balance.split('.')
  const decimals = array[1].substring(0, 5)
  return array[0].concat(`.${decimals}`)
}

export async function getMarketplaceStats() {
  const { marketPlaceContract } = await getContracts();

  const totalItems = await marketPlaceContract.getItemsCount();
  const itemsSold = await marketPlaceContract.getItemsSold();
  const marketplaceBalance = await marketPlaceContract.getMarketPlaceBalance();
  const listingFee = await marketPlaceContract.getListingFee();

  return {
    totalItems: totalItems.toString(),
    itemsSold: itemsSold.toString(),
    marketplaceBalance: ethers.formatEther(marketplaceBalance.toString()),
    listingFee: ethers.formatEther(listingFee.toString())
  }
}

export async function connectWallet() {
  
  let accountInfo = await getAccountInfo();

  if (accountInfo.account) {
    window.ethereum.on("accountsChanged", async (accounts) => {
      let updatedAccountInfo = await getAccountInfo();
      DotNet.invokeMethodAsync("Seckim.NFTMarketplace.App.Application", "UpdateAccountInfo", updatedAccountInfo);
    });
  }

  return accountInfo;
}

export async function buyNFT(tokenId) {
  const { marketPlaceContract } = await getContracts();
  const price = ethers.parseUnits('0.0001', 'ether');
  const transaction = await marketPlaceContract.buyNFT(
    NFTAddress,
    tokenId,
    { gasLimit: 3000000, value: price.toString() }
  );

  await transaction.wait();

  return { transaction: JSON.stringify(transaction) };
}

export async function resellNFT(tokenId) {
  const { nftContract, marketPlaceContract } = await getContracts();
  const price = ethers.parseUnits('0.0001', 'ether');

  await nftContract.approve(MKTAddress, tokenId);

  const transaction = await marketPlaceContract.resellNFT(
    NFTAddress,
    tokenId,
    { gasLimit: 3000000, value: price.toString() }
  );

  await transaction.wait();

  return { transaction: JSON.stringify(transaction) };
}

export async function fetchAllMarketItems() {
  const { nftContract, marketPlaceContract } = await getContracts();
  let currentId = await nftContract.getCurrentToken();
  let currentTokenId = BigInt(currentId);
  console.log('currentTokenId: ' + currentTokenId);
  let tokens = [];
  for (let i = 1; i <= currentTokenId; i++) {
    const token = await marketPlaceContract.getTokenForId(i);
    tokens = [...tokens, token];
  }

  return await Promise.all(tokens.map(async token => {
    let price = ethers.formatUnits(token.price.toString(), 'ether');
    let item = {
      price,
      tokenId: token.tokenId.toString(),
      seller: token.seller,
      owner: token.owner,
      name: token.name,
      tokenURI: token.tokenURI,
      isListed: token.isListed
    };

    return item;
  }));
}

export async function mintNFT(uri) {

  const { nftContract } = await getContracts(); 

  console.log(nftContract);

  const currentId = await nftContract.getCurrentToken();
  const tokenId = BigInt(currentId);
  await nftContract.mint(uri);

  console.log("Minted NFT #" + tokenId);

  const strtokenId = tokenId.toString();

  return { tokenId: strtokenId };
}

export async function listNFT(tokenId, name, tokenUri) {

  const { marketPlaceContract } = await getContracts();

  console.log('listing NFT: tokenId=' + tokenId + ', name=' + name + ', tokenUri=' + tokenUri);

  const listingPrice = ethers.parseUnits('0.0001', 'ether');

  console.log('listingPrice: ' + listingPrice);

  const transaction = await marketPlaceContract.listNFT(
    NFTAddress,
    tokenId,
    name,
    tokenUri,
    { gasLimit: 3000000, value: listingPrice.toString() }
  );

  await transaction.wait();

  return { transaction: JSON.stringify(transaction) };
}

async function getContracts() {
  const signer = await provider.getSigner();
  const marketPlaceContract = new ethers.Contract(MKTAddress, MarketplaceJson.abi, signer);
  const nftContract = new ethers.Contract(NFTAddress, NftJson.abi, signer);

  console.log('NFT Contract getContracts: ' + nftContract);

  return {
    marketPlaceContract,
    nftContract
  }
}

async function getAccountInfo() {
  const signer = await provider.getSigner();

  const account = await signer.getAddress();
  const balance = await provider.getBalance(account);
  const formattedBalance = ethers.formatEther(balance);

  return {
    account,
    balance: balanceInEth(formattedBalance)
  }
}