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