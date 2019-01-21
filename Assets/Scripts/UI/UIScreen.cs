using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour, IPointerClickHandler {

	[Header("Game Data")]
	public int StreamerIndex;
	public bool IsInGame;

	[Header("UI Items")]
	public Image Frame;
	public Image Background;
	public Image StreamerImage;
	public GameObject MenuDescription;
	public Text MenuDescriptionName;

	public Main TileMatchGame;

	public Animator _Animator;

	private Streamer currentStreamer;

	public void OnPointerClick(PointerEventData eventData) {
		if(!IsInGame) {
			GameMode.Instance.LoadView(StreamerIndex);
		} else {
			if(eventData.clickCount == 2) {
				if(TileMatchGame == null) {
					TileMatchGame = (Main)FindObjectOfType(typeof(Main));
				}
				TileMatchGame.Clear();
			}
		}
	}

	public void Shake() {
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

		_Animator.SetTrigger("Angry");

		StartCoroutine(ShakeCoroutine());
	}

	// Use this for initialization
	private void Awake () {
		if(GameMode.Instance.SelectStreamer != -1) {
			StreamerIndex = GameMode.Instance.SelectStreamer;
		}
		currentStreamer = GameMode.Instance.GetStreamer(StreamerIndex);
	}

	private void Start() {
		Frame.sprite = GameMode.Instance.GetResourceManager().Get<Sprite>("Sprites/Menu/Frame");
		Background.sprite = GameMode.Instance.GetResourceManager().Get<Sprite>(string.Format("Sprites/{0}/room", currentStreamer.Name));
		StreamerImage.sprite = GameMode.Instance.GetResourceManager().Get<Sprite>(string.Format("Sprites/{0}/idle_01", currentStreamer.Name));

		if(IsInGame) {
			MenuDescription.SetActive(false);
		} else {
			MenuDescriptionName.text = currentStreamer.Name;
		}
		_Animator.runtimeAnimatorController = GameMode.Instance.GetResourceManager().Get<RuntimeAnimatorController>(
			string.Format("Anims/{0}/AnimController", currentStreamer.Name));

	}
	
	// Update is called once per frame
	private void Update () {
		_Animator.SetInteger("Combo", GameMode.Instance.GetCombo());
	}

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;
	private float temp_shake_intensity = 0;
	private IEnumerator ShakeCoroutine() {
		while (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
			yield return null;
		}
		yield return null;
	}
}
