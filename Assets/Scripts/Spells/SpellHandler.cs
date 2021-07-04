using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandler : MonoBehaviour
{
	public PlayersController playersController;
	public Spell spell;
	public Spell[] spells;
	public bool[] unlocked;
	public ParticleSystem electricPSPlayer0;
	public ParticleSystem electricPSPlayer1;
	public Animator spellActiveLightAnim;

	KeyCode resize = KeyCode.Alpha1, open = KeyCode.Alpha2, lightSwitch = KeyCode.Alpha3;
	List<KeyCode> keyCodes;

	// Start is called before the first frame update
	void Start()
	{
		foreach (Spell spell in spells)
		{
			spell.transform.gameObject.SetActive(false);
		}
		unlocked = new bool[spells.Length];

		keyCodes = new List<KeyCode>();
		keyCodes.Add(resize);
		keyCodes.Add(open);
		keyCodes.Add(lightSwitch);
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = keyCodes.Count - 1; i >= 0; i--)
		{
			if (Input.GetKeyDown(keyCodes[i]) && unlocked[i])
			{
				spell = spells[i];
				MakeActive();
			}
		}

		if (Input.GetMouseButton(0))
		{
			switch (playersController.enabledPlayer)
			{
				case 0:
					var startLife0 = electricPSPlayer0.main;
					startLife0.startLifetime = 0.8f;
					spellActiveLightAnim.SetBool("player1SpellActive", true);
					break;

				case 1:
					spellActiveLightAnim.SetBool("player2SpellActive", true);
					var startLife1 = electricPSPlayer1.main;
					startLife1.startLifetime = 0.8f;
					break;
			}
		}
		else
		{
            var startLife0 = electricPSPlayer0.main;
            var startLife1 = electricPSPlayer1.main;
            startLife0.startLifetime = 0;
            startLife1.startLifetime = 0;
			spellActiveLightAnim.SetBool("player1SpellActive", false);
			spellActiveLightAnim.SetBool("player2SpellActive", false);
		}

	}

	public void SpellSetup(GameObject[] players, Camera cam)
	{
		foreach (Spell spell in spells)
		{
			if (this.spell = spell)
			{
				spell.Setup(cam.transform, players);
			}
		}
	}

	void MakeActive()
	{
		foreach (Spell spell in spells)
		{
			if (this.spell == spell)
			{
				spell.gameObject.SetActive(true);
			}
			else
			{
				spell.gameObject.SetActive(false);
			}
		}
	}

	public void UnlockNextSpell()
	{
		for (int i = 0; i < unlocked.Length; i++)
		{
			if (!unlocked[i])
			{
				unlocked[i] = true;
				return;
			}
		}
	}
}