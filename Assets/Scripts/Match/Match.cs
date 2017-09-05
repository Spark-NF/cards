using System;

public class Match
{
	private MatchSide[] _sides;
	private int _currentSide;
	private MatchStep _currentStep;

	public Match(MatchSide[] sides)
	{
		_sides = sides;

		var rng = new Random();
		_currentSide = rng.Next(0, _sides.Length);
	}

	public void NextStep()
	{
		if (_currentStep == MatchStep.End)
		{
			NextTurn();
			return;
		}

		BeginStep(_currentStep + 1);
	}

	public void BeginStep(MatchStep step)
	{
		_currentStep = step;
		_sides[_currentSide].BeginStep(_currentStep);
	}

	public void EndStep()
	{
		_sides[_currentSide].EndStep(_currentStep);
	}

	public void NextTurn()
	{
		_currentSide = (_currentSide + 1) % _sides.Length;
		BeginStep(MatchStep.Untap);
	}
}
