import { connectWallet, mintNFT, listNFT, fetchAllMarketItems, buyNFT, resellNFT, getMarketplaceStats } from "./marketplace.js";

window.connectWallet = async () => {
  const wallet = await connectWallet();
  console.log(wallet);
  return wallet;
}

window.getMarketplaceStats = async () => {
  const stats = await getMarketplaceStats();
  console.log(stats);
  return stats;
}

window.resellNFT = async (tokenId) => {
const nft = await resellNFT(tokenId);
  console.log(nft);
  return nft;
}

window.buyNFT = async (tokenId) => {
  const nft = await buyNFT(tokenId);
  console.log(nft);
  return nft;
}

window.fetchAllMarketItems = async () => {
  const items = await fetchAllMarketItems();
  console.log(items);
  return items;
}

window.mintNFT = async (uri) => {
  const nft = await mintNFT(uri);
  console.log(nft);
  return nft;
}

window.listNFT = async (tokenId, name, tokenUri) => {
const nft = await listNFT(tokenId, name, tokenUri);
  console.log(nft);
  return nft;
}