import { connectWallet } from "./marketplace.js";

window.connectWallet = async () => {
  const wallet = await connectWallet();
  console.log(wallet);
  return wallet;
}