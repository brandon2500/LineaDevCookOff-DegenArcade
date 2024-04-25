using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class TokenScript : MonoBehaviour
{
    public ScoreManager scoreManager;

    public GameObject HasNotClaimedState;
    public GameObject ClaimingState;
    public GameObject HasClaimedState;

    private int ticketsToClaim = 50;

    [SerializeField] public TMPro.TextMeshProUGUI ticketsEarnedText;

    [SerializeField] private TMPro.TextMeshProUGUI tokenBalanceText;

    private const string DROP_ERC20_CONTRACT = "0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae";

    void start()
    {
        HasNotClaimedState.SetActive(false);
        ClaimingState.SetActive(false);
        HasClaimedState.SetActive(false);

    }

    void Update()
    {
        ticketsEarnedText.text = "TICKETS EARNED:" + scoreManager.score.ToString();
        ticketsToClaim = scoreManager.score;
    }

   public async void GetTokenBalance()
    {
        try
        {
            var address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            var data = await contract.ERC20.BalanceOf(address);
            tokenBalanceText.text = "$TICKETS:" + data.displayValue;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public void ResetBalance()
    {
        tokenBalanceText.text = "$TICKETS: 0";
    }

    public async void MintERC20()
    {
        try
        {
            Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
            HasNotClaimedState.SetActive(false);
            ClaimingState.SetActive(true);
            var results = await contract.ERC20.Claim(ticketsToClaim.ToString());
            GetTokenBalance();
            ClaimingState.SetActive(false);
            HasNotClaimedState.SetActive (false);
            HasClaimedState.SetActive(true);
        }
        catch
        {

        }
    }
}
