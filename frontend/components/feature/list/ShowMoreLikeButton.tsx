"use client";

import { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Heart, ThumbsUp, Sparkles } from 'lucide-react';

interface ShowMoreLikeButtonProps {
  onClick: () => void;
  variant?: 'icon' | 'text' | 'full';
  size?: 'sm' | 'md' | 'lg';
  className?: string;
  disabled?: boolean;
}

export function ShowMoreLikeButton({
  onClick,
  variant = 'icon',
  size = 'md',
  className = '',
  disabled = false
}: ShowMoreLikeButtonProps) {
  const [isClicked, setIsClicked] = useState(false);
  const [showFeedback, setShowFeedback] = useState(false);
  
  const handleClick = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    
    if (disabled || isClicked) return;
    
    setIsClicked(true);
    setShowFeedback(true);
    onClick();
    
    // Reset after animation
    setTimeout(() => {
      setShowFeedback(false);
    }, 2000);
    
    // Allow clicking again after cooldown
    setTimeout(() => {
      setIsClicked(false);
    }, 5000);
  };
  
  const sizeClasses = {
    sm: 'h-6 w-6 p-1',
    md: 'h-8 w-8 p-1.5',
    lg: 'h-10 w-10 p-2'
  };
  
  const iconSizes = {
    sm: 14,
    md: 18,
    lg: 22
  };
  
  if (variant === 'icon') {
    return (
      <button
        onClick={handleClick}
        disabled={disabled}
        title="Pokaż więcej takich ofert"
        className={`
          relative inline-flex items-center justify-center
          rounded-full transition-all duration-200
          ${sizeClasses[size]}
          ${isClicked 
            ? 'bg-primary text-primary-foreground scale-110' 
            : 'bg-secondary/50 hover:bg-secondary text-secondary-foreground hover:scale-105'
          }
          ${disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer'}
          ${className}
        `}
      >
        <ThumbsUp 
          size={iconSizes[size]} 
          className={`transition-transform ${isClicked ? 'scale-110' : ''}`}
        />
        
        {/* Feedback animation */}
        {showFeedback && (
          <span className="absolute -top-1 -right-1 flex h-3 w-3">
            <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-primary opacity-75"></span>
            <span className="relative inline-flex rounded-full h-3 w-3 bg-primary"></span>
          </span>
        )}
      </button>
    );
  }
  
  if (variant === 'text') {
    return (
      <button
        onClick={handleClick}
        disabled={disabled}
        className={`
          inline-flex items-center gap-1.5 text-sm
          transition-colors duration-200
          ${isClicked 
            ? 'text-primary font-medium' 
            : 'text-muted-foreground hover:text-primary'
          }
          ${disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer'}
          ${className}
        `}
      >
        <ThumbsUp size={14} />
        <span>Więcej takich</span>
        {showFeedback && <Sparkles size={12} className="text-primary animate-pulse" />}
      </button>
    );
  }
  
  // Full variant
  return (
    <Button
      onClick={handleClick}
      disabled={disabled}
      variant={isClicked ? 'default' : 'outline'}
      size="sm"
      className={`gap-2 ${className}`}
    >
      <ThumbsUp size={16} />
      <span>Pokaż więcej takich</span>
      {showFeedback && <Sparkles size={14} className="animate-pulse" />}
    </Button>
  );
}

/**
 * Heart/favorite variant for different UI context
 */
export function LikeOfferButton({
  onClick,
  isLiked = false,
  size = 'md',
  className = ''
}: {
  onClick: () => void;
  isLiked?: boolean;
  size?: 'sm' | 'md' | 'lg';
  className?: string;
}) {
  const [localLiked, setLocalLiked] = useState(isLiked);
  
  const handleClick = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    
    setLocalLiked(!localLiked);
    onClick();
  };
  
  const sizeClasses = {
    sm: 'h-6 w-6',
    md: 'h-8 w-8',
    lg: 'h-10 w-10'
  };
  
  const iconSizes = {
    sm: 14,
    md: 18,
    lg: 22
  };
  
  return (
    <button
      onClick={handleClick}
      title={localLiked ? 'Usuń z polubionych' : 'Pokaż więcej takich'}
      className={`
        inline-flex items-center justify-center
        rounded-full transition-all duration-200
        ${sizeClasses[size]}
        hover:scale-110
        ${localLiked 
          ? 'text-red-500' 
          : 'text-muted-foreground hover:text-red-400'
        }
        ${className}
      `}
    >
      <Heart 
        size={iconSizes[size]}
        fill={localLiked ? 'currentColor' : 'none'}
        className="transition-all"
      />
    </button>
  );
}
