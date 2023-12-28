import { connectWallet, mintNFT, listNFT } from "./marketplace.js";

window.connectWallet = async () => {
  const wallet = await connectWallet();
  console.log(wallet);
  return wallet;
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