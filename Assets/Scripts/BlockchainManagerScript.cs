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

    public GameObject ERC20TokenBalanceText;


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

        OnLoggedIn.Invoke(Address);

        }

    public async void ClaimToken()
    {
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x9F5d12c4A61F39AAd3e6b3973db70FCe1244e6DA");

        var data = await contract.ERC20.Claim(_score);
    }

}
