using UnityEngine;
using ZXing;
using UnityEngine.UI;

public class QRScan : MonoBehaviour
{
    public GameObject ButtonConfirm;
    public Test test;
    BarcodeReader reader;

    private bool isScanning = false;
    private float interval = 0;

    private WebCamTexture webCamTexture;

    private Color32[] data = new Color32[1];

    public RawImage cameraTexture;

    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToPortrait = false;
        DeviceInit();
    }
    public void EnableScanning(bool enabled)
    {
        isScanning = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScanning)
        {
            interval += Time.deltaTime;
            if(interval >= 0.1f)
            {
                interval = 0;
                ScanQRCode();
            }
        }
    }

    void DeviceInit()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webCamTexture = new WebCamTexture(devices[0].name, 300, 300);
        cameraTexture.texture = webCamTexture;

        webCamTexture.Play();

        reader = new BarcodeReader();
    }

    void ScanQRCode()
    {
        data = webCamTexture.GetPixels32();

        Result result = reader.Decode(data, webCamTexture.width, webCamTexture.height);

        if(result != null)
        {
            test.CheckAnswer(result.Text);
            ButtonConfirm.SetActive(true);
            isScanning = false;
            Handheld.Vibrate();
        }
    }

    public void StartScan()
    {
        isScanning = true;
    }
}
