﻿
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class PromotionPanel
	{
		private bool isWhite;
		private bool isOpen;
		private int x;
		private int y;
		private Move.PromotionType? hoverSquare;
		private Move.PromotionType? selectedSquare;

		private const int PROMOTION_PANEL_WIDTH = 293;
		private const int PROMOTION_PANEL_HEIGHT = 100;

		private const int QUEEN_OFFSET_X = 10;
		private const int ROOK_OFFSET_X = 10 + 70;
		private const int KNIGHT_OFFSET_X = 10 + 70 * 2;
		private const int BISHOP_OFFSET_X = 10 + 70 * 3;
		private const int PIECE_OFFSET_Y = 8;

		private PromotionPanel(
			bool isWhite,
			bool isOpen,
			int x,
			int y,
			Move.PromotionType? hoverSquare,
			Move.PromotionType? selectedSquare)
		{
			this.isWhite = isWhite;
			this.isOpen = isOpen;
			this.x = x;
			this.y = y;
			this.hoverSquare = hoverSquare;
			this.selectedSquare = selectedSquare;
		}

		public static PromotionPanel GetPromotionPanel(bool isWhite)
		{
			return new PromotionPanel(
				isWhite: isWhite,
				isOpen: false,
				x: 0,
				y: 0,
				hoverSquare: null,
				selectedSquare: null);
		}

		public PromotionPanel ProcessFrame(
			bool isOpen,
			int x,
			int y,
			Move.PromotionType? hoverSquare,
			Move.PromotionType? selectedSquare)
		{
			return new PromotionPanel(
				isWhite: this.isWhite,
				isOpen: isOpen,
				x: x,
				y: y,
				hoverSquare: hoverSquare,
				selectedSquare: selectedSquare);
		}

		/// <summary>
		/// Returns the piece that the mouse is hovering over, if any
		/// </summary>
		public static Move.PromotionType? IsHoverOverSquare(
			int promotionPanelX,
			int promotionPanelY,
			IMouse mouse,
			IDisplayProcessing<ChessImage> displayProcessing)
		{
			int imageWidth = displayProcessing.GetWidth(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;
			int imageHeight = displayProcessing.GetHeight(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;

			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();

			if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y)
				return null;
			if (mouseY > promotionPanelY - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y + imageHeight)
				return null;

			int mouseXRelativeToPanel = mouseX - promotionPanelX;

			if (QUEEN_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= QUEEN_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToQueen;
			if (ROOK_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= ROOK_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToRook;
			if (KNIGHT_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= KNIGHT_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToKnight;
			if (BISHOP_OFFSET_X <= mouseXRelativeToPanel && mouseXRelativeToPanel <= BISHOP_OFFSET_X + imageWidth)
				return Move.PromotionType.PromoteToBishop;
			return null;
		}

		public static bool IsHoverOverPanel(
			int promotionPanelX,
			int promotionPanelY, 
			IMouse mouse)
		{
			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();
			
			if (mouseX < promotionPanelX)
				return false;
			if (mouseX > promotionPanelX + PROMOTION_PANEL_WIDTH)
				return false;
			if (mouseY < promotionPanelY - PROMOTION_PANEL_HEIGHT)
				return false;
			if (mouseY > promotionPanelY)
				return false;
			return true;
		}

		private static int GetXOffset(Move.PromotionType promotionType)
		{
			switch (promotionType)
			{
				case Move.PromotionType.PromoteToQueen:
					return QUEEN_OFFSET_X;
				case Move.PromotionType.PromoteToRook:
					return ROOK_OFFSET_X;
				case Move.PromotionType.PromoteToKnight:
					return KNIGHT_OFFSET_X;
				case Move.PromotionType.PromoteToBishop:
					return BISHOP_OFFSET_X;
				default:
					throw new Exception();
			}
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			if (!this.isOpen)
				return;

			int imageWidth = displayOutput.GetWidth(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;
			int imageHeight = displayOutput.GetHeight(ChessImage.WhitePawn) * ChessImageUtil.ChessPieceScalingFactor / 128;

			displayOutput.DrawRectangle(
				x: this.x,
				y: this.y - PROMOTION_PANEL_HEIGHT,
				width: PROMOTION_PANEL_WIDTH,
				height: PROMOTION_PANEL_HEIGHT,
				color: new DTColor(255, 245, 171),
				fill: true);

			displayOutput.DrawText(
				x: this.x + 90,
				y: this.y - 10,
				text: "Promote to:",
				font: ChessFont.ChessFont14Pt,
				color: DTColor.Black());

			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? ChessImage.WhiteQueen : ChessImage.BlackQueen,
				x: this.x + QUEEN_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? ChessImage.WhiteRook : ChessImage.BlackRook,
				x: this.x + ROOK_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? ChessImage.WhiteKnight : ChessImage.BlackKnight,
				x: this.x + KNIGHT_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);
			displayOutput.DrawImageRotatedClockwise(
				image: this.isWhite ? ChessImage.WhiteBishop : ChessImage.BlackBishop,
				x: this.x + BISHOP_OFFSET_X,
				y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
				degreesScaled: 0,
				scalingFactorScaled: ChessImageUtil.ChessPieceScalingFactor);

			if (this.hoverSquare != null && (this.selectedSquare == null || this.selectedSquare.Value != this.hoverSquare.Value))
			{
				int hoverXOffset = GetXOffset(promotionType: this.hoverSquare.Value);

				displayOutput.DrawRectangle(
					x: this.x + hoverXOffset,
					y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
					width: imageWidth,
					height: imageHeight,
					color: new DTColor(0, 0, 128, 50),
					fill: true);
			}

			if (this.selectedSquare != null)
			{
				int selectedXOffset = GetXOffset(promotionType: this.selectedSquare.Value);
				
				displayOutput.DrawRectangle(
					x: this.x + selectedXOffset,
					y: this.y - PROMOTION_PANEL_HEIGHT + PIECE_OFFSET_Y,
					width: imageWidth,
					height: imageHeight,
					color: new DTColor(0, 0, 170, 150),
					fill: true);
			}
		}
	}
}
