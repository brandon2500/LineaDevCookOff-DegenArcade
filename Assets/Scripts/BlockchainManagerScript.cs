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

    public TextMeshProUGUI ERC20TokenBalanceText2;

    public Button claimCardButton;

    public GameObject ClaimingState;
    public GameObject HasClaimedState;


    void Start()
    {
        var sdk = ThirdwebManager.Instance.SDK;
        ClaimingState.SetActive(false);
        HasClaimedState.SetActive(false);
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

        claimTokenButton.interactable = false;

        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
        ClaimingState.SetActive(true);

        var data = await contract.ERC20.Claim(_score);
        ClaimingState.SetActive(false);
        HasClaimedState.SetActive(true);

    }


    public async void MintFrogPlush()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x8aa9f12Bdf2fbb8A2450DFc4588C10fBeDAfDEDa");
        var result = await contract.ERC721.Claim(1);

        GetTokenBalance();
    }    

    public async void ApproveAllowancePlushFrog()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
        string amount = "10001";
        var data = await contract.ERC20.SetAllowance("0x8aa9f12Bdf2fbb8A2450DFc4588C10fBeDAfDEDa", amount);

        MintFrogPlush();
    }

    public async void MintFoxPlush()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x889f147a51E01924ae81ac4d82B127ccBBc0717B");
        var result = await contract.ERC721.Claim(1);

        GetTokenBalance();
    }

    public async void ApproveAllowanceForFox()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
        string amount = "2501";
        var data = await contract.ERC20.SetAllowance("0x889f147a51E01924ae81ac4d82B127ccBBc0717B", amount);

        MintFoxPlush();
    }

    public async void MintDinoPlush()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x662dC090db2937D79715FE7776d0B50891356E0c");
        var result = await contract.ERC721.Claim(1);

        GetTokenBalance();
    }

    public async void ApproveAllowanceForDino()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
        string amount = "501";
        var data = await contract.ERC20.SetAllowance("0x662dC090db2937D79715FE7776d0B50891356E0c", amount);

        MintDinoPlush();
    }

    public void DisableText()
    {
        ClaimingState.SetActive(false);
        HasClaimedState.SetActive(false);
    }

    public async void GetTokenBalance()
    {
        try
        {
            var address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
            Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x7BB8A91eEd4b2d987C53A01AB009D84d9C8449ae");
            var data = await contract.ERC20.BalanceOf(address);
            ERC20TokenBalanceText.text = "$TICKETS:" + data.displayValue;
            ERC20TokenBalanceText2.text = "$TICKETS:" + data.displayValue.ToString();
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
