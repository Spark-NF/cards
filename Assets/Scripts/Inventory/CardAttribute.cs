public enum CardAttribute
{
	Deathtouch, // Any attack above 0 kills
	Defender, // Can't attack
	FirstStrike, // Attack first (may kill without receiving damage)
	DoubleStrike, // First strike + normal strike
	Flying, // Can't be blocked by non-flying and non-reach
	Haste, // Can't tap without waiting
	Intimidate, // Can't be blocked by a different color card
	Reach, // Can block flying
	Shroud, // Can't use spells or abilities on it
	Trample, // Difference in damage when blocking hurts the other player
	Vigilance // Can attack then still block in the same turn
}
