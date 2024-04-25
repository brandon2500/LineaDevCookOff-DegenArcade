using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;

public class BlockchainManagerScript : MonoBehaviour
{
    public UnityEvent<string> OnLoggedIn;

    public string Address { get; private set; }

    public static BlockchainManagerScript Instance { get; private set; }

    public Button claimTokenButton;

    public TextMeshProUGUI claimTokenButtonText;

    public ScoreManager scoreManagerRef;

    private string _score;

    public TextMeshProUGUI ERC20TokenBalanceText;

    public GameObject noCardPanel;

    public GameObject hasCardPanel;

    public Button claimCardButton;


    void Start()
    {
        var sdk = ThirdwebManager.Instance.SDK;
    }

    void Update()
    {
        _score = scoreManagerRef.score.ToString();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public async void Login(string authProvider)
    {
        AuthProvider provider = AuthProvider.Google;
        switch (authProvider)
        {
            case "google":
                provider = AuthProvider.Google;
                break;
            case "apple":
                provider = AuthProvider.Apple;
                break;
            case "facebook":
                provider = AuthProvider.Facebook;
                break;
            }

        var connection = new WalletConnection(
            provider: WalletProvider.SmartWallet,
            chainId: 59141,
            authOptions: new AuthOptions(authProvider: provider)
        );

        Address = await ThirdwebManager.Instance.SDK.Wallet.Connect(connection);

        var contract = ThirdwebManager.Instance.SDK.GetContract("0x25717214e6472B19A95Cc8EC56E28C56d2b07d36");
        var balance = await contract.ERC721.BalanceOf(Address);

        if(balance == 0)
        {
        
        }
        else
        {
            
        }

        InvokeOnLoggedIn();


        }

    void InvokeOnLoggedIn()
    {
        OnLoggedIn.Invoke(Address);
        GetTokenBalance();
    }

    public async void ClaimToken()
    {
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");

        var data = await contract.ERC20.Claim(_score);

        claimTokenButton.interactable = false;
    }

    public async void GetTokenBalance()
    {
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");

        var balance = await contract.ERC20.BalanceOf(Address);


        ERC20TokenBalanceText.text = balance.displayValue;
    }

    public async void MintFrogPlush()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x8aa9f12Bdf2fbb8A2450DFc4588C10fBeDAfDEDa");
        var result = await contract.ERC721.Claim(1);
    }    

    public async void ApproveAllowance()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
        string amount = "10000000000";
        var data = await contract.ERC20.SetAllowance("0x8aa9f12Bdf2fbb8A2450DFc4588C10fBeDAfDEDa", amount);

        MintFrogPlush();
    }

}
